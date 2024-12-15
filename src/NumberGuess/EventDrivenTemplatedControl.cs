using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace NumberGuess
{
    public interface IControlEvent
    {
        string ControlName { get; }

        Action<Control> Connector { get; }

        Action<Control>? Disconnector { get; }
    }

    public interface IControlEvent<T> : IControlEvent where T : Control
    {
        new Action<T> Connector { get; }

        new Action<T>? Disconnector { get; }
    }

    public class ControlEvent<T> : IControlEvent<T> where T : Control
    {
        public string ControlName { get; }

        public Action<Control> Connector { get; }

        public Action<Control>? Disconnector { get; }

        Action<T> IControlEvent<T>.Connector => Connector;

        Action<T>? IControlEvent<T>.Disconnector => Disconnector;

        public ControlEvent(string controlName, Action<T> connector, Action<T>? disconnector = null)
        {
            ControlName = controlName;
            Connector = c => connector((T)c);

            if (disconnector != null)
            {
                Disconnector = d => disconnector((T)d);
            }
        }
    }

    public interface IEventDrivenTemplatedControl
    {
        Control? GetControl(string name);
    }

    public abstract class EventDrivenTemplatedControl : TemplatedControl, IEventDrivenTemplatedControl
    {
        private class EventConnection
        {
            public IControlEvent ControlEvent { get; }

            public Control Control { get; }

            public EventConnection(IControlEvent controlEvent, Control control)
            {
                ControlEvent = controlEvent;
                Control = control;
            }
        }

        private List<EventConnection> _eventConnections = new();

        protected abstract IControlEvent[]? GetEvents();

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            var controlEvents = GetEvents();

            if (controlEvents == null)
            {
                return;
            }

            for (var i = 0; i < controlEvents.Length; i++)
            {
                var control = (Control?)e.NameScope.Find(controlEvents[i].ControlName) ?? throw new Exception($"Could not find control '{controlEvents[i].ControlName}'.");

                _eventConnections.Add(new EventConnection(controlEvents[i], control));
            }
        }

        protected override void OnLoaded(RoutedEventArgs e)
        {
            base.OnLoaded(e);

            for (var i = 0; i < _eventConnections.Count; i++)
            {
                _eventConnections[i].ControlEvent.Connector(_eventConnections[i].Control);
            }
        }

        protected override void OnUnloaded(RoutedEventArgs e)
        {
            base.OnUnloaded(e);

            for (var i = 0; i < _eventConnections.Count; i++)
            {
                _eventConnections[i].ControlEvent.Disconnector?.Invoke(_eventConnections[i].Control);
            }
        }

        public Control? GetControl(string name)
        {
            var result = _eventConnections.FirstOrDefault(i => i.Control.Name == name);

            return result?.Control;
        }

        protected void CallModelMethod<T>(string methodName, object?[]? parameters = null) where T : ViewModelBase
        {
            CallModelMethod(typeof(T), methodName, parameters);
        }

        protected void CallModelMethod(Type modelType, string methodName, object?[]? parameters = null)
        {
            if (DataContext == null
                || !DataContext.GetType().IsAssignableTo(modelType))
            {
                return;
            }

            var methodInfo = modelType.GetMethod(methodName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            if (methodInfo == null)
            {
                return;
            }

            methodInfo.Invoke(DataContext, parameters);
        }

        protected async Task CallModelMethodAsync<T>(string methodName, object?[]? parameters = null) where T : ViewModelBase
        {
            await CallModelMethodAsync(typeof(T), methodName, parameters);
        }

        protected async Task CallModelMethodAsync(Type modelType, string methodName, object?[]? parameters = null)
        {
            if (DataContext == null
                || !DataContext.GetType().IsAssignableTo(modelType))
            {
                return;
            }

            var methodInfo = modelType.GetMethod(methodName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            if (methodInfo == null)
            {
                return;
            }

            var task = (Task?)methodInfo.Invoke(DataContext, parameters);

            if (task == null)
            {
                return;
            }

            await task;
        }
    }
}
