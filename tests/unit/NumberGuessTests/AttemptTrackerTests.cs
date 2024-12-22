using FluentAssertions;
using NumberGuess;

namespace NumberGuessTests
{

    public class AttemptTrackerTests
    {
        [Fact]
        public void When_constructor_called_then_properties_set()
        {
            var sut = new AttemptTracker();

            sut.Inputs.Count.Should().Be(0);
            sut.State.Should().Be(AttemptState.Uninitialized);
            sut.CanInput.Should().BeFalse();
            sut.CanSubmit.Should().BeFalse();
            sut.DigitCount.Should().Be(0);
            sut.DigitPlace.Should().Be(0);
        }

        [Fact]
        public void When_start_called_with_valid_digit_count_then_properties_set()
        {
            var digitCount = 4;

            var sut = new AttemptTracker();

            sut.Start(digitCount);

            sut.Inputs.Count.Should().Be(digitCount);
            sut.State.Should().Be(AttemptState.Input);
            sut.CanInput.Should().BeTrue();
            sut.CanSubmit.Should().BeFalse();
            sut.DigitCount.Should().Be(digitCount);
            sut.DigitPlace.Should().Be(0);
        }

        [Fact]
        public void When_start_called_with_zero_digit_count_then_throw_argumentoutofrangeexception()
        {
            var digitCount = 0;

            var sut = new AttemptTracker();

            Action action = () => sut.Start(digitCount);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void When_start_called_with_negative_digit_count_then_throw_argumentoutofrangeexception()
        {
            var digitCount = -1;

            var sut = new AttemptTracker();

            Action action = () => sut.Start(digitCount);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void When_start_called_with_greater_than_maximum_digit_count_then_throw_argumentoutofrangeexception()
        {
            var digitCount = AttemptTracker.MaxDigitCount + 1;

            var sut = new AttemptTracker();

            Action action = () => sut.Start(digitCount);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void When_setinput_called_before_start_then_throw_invalidoperationexception()
        {
            var input = '0';

            var sut = new AttemptTracker();

            Action action = () => sut.SetInput(input);

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void When_first_setinput_called_then_properties_set()
        {
            var digitCount = 4;
            var input = '0';

            var sut = new AttemptTracker();

            sut.Start(digitCount);
            sut.SetInput(input);

            sut.State.Should().Be(AttemptState.Input);
            sut.CanInput.Should().BeTrue();
            sut.CanSubmit.Should().BeFalse();
            sut.DigitPlace.Should().Be(1);
            sut.Inputs[0].Should().Be(input);
            sut.Inputs[1].Should().BeNull();
            sut.Inputs[2].Should().BeNull();
            sut.Inputs[3].Should().BeNull();
        }

        [Fact]
        public void When_last_setinput_called_then_properties_set()
        {
            var digitCount = 4;
            var input0 = '0';
            var input1 = '1';
            var input2 = '2';
            var input3 = '3';

            var sut = new AttemptTracker();

            sut.Start(digitCount);
            sut.SetInput(input0);
            sut.SetInput(input1);
            sut.SetInput(input2);
            sut.SetInput(input3);

            sut.State.Should().Be(AttemptState.Submit);
            sut.CanInput.Should().BeFalse();
            sut.CanSubmit.Should().BeTrue();
            sut.DigitPlace.Should().Be(digitCount);
            sut.Inputs[0].Should().Be(input0);
            sut.Inputs[1].Should().Be(input1);
            sut.Inputs[2].Should().Be(input2);
            sut.Inputs[3].Should().Be(input3);
        }

        [Fact]
        public void When_setinput_called_more_times_that_digitcount_then_throw_invalidoperationexception()
        {
            var digitCount = 4;
            var input0 = '0';
            var input1 = '1';
            var input2 = '2';
            var input3 = '3';
            var input4 = '4';

            var sut = new AttemptTracker();

            sut.Start(digitCount);
            sut.SetInput(input0);
            sut.SetInput(input1);
            sut.SetInput(input2);
            sut.SetInput(input3);

            Action action = () => sut.SetInput(input4);

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void When_back_called_before_start_then_throw_invalidoperationexception()
        {
            var sut = new AttemptTracker();

            Action action = () => sut.Back();

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void When_back_called_before_first_setinput_then_setproperties()
        {
            var digitCount = 4;

            var sut = new AttemptTracker();

            sut.Start(digitCount);
            sut.Back();

            sut.State.Should().Be(AttemptState.Input);
            sut.CanInput.Should().BeTrue();
            sut.CanSubmit.Should().BeFalse();
            sut.DigitPlace.Should().Be(0);
            sut.Inputs[0].Should().BeNull();
            sut.Inputs[1].Should().BeNull();
            sut.Inputs[2].Should().BeNull();
            sut.Inputs[3].Should().BeNull();
        }

        [Fact]
        public void When_back_called_after_first_setinput_then_setproperties()
        {
            var digitCount = 4;
            var input = '0';

            var sut = new AttemptTracker();

            sut.Start(digitCount);
            sut.SetInput(input);
            sut.Back();

            sut.State.Should().Be(AttemptState.Input);
            sut.CanInput.Should().BeTrue();
            sut.CanSubmit.Should().BeFalse();
            sut.DigitPlace.Should().Be(0);
            sut.Inputs[0].Should().BeNull();
            sut.Inputs[1].Should().BeNull();
            sut.Inputs[2].Should().BeNull();
            sut.Inputs[3].Should().BeNull();
        }

        [Fact]
        public void When_back_called_after_second_setinput_then_setproperties()
        {
            var digitCount = 4;
            var input0 = '0';
            var input1 = '1';

            var sut = new AttemptTracker();

            sut.Start(digitCount);
            sut.SetInput(input0);
            sut.SetInput(input1);
            sut.Back();

            sut.State.Should().Be(AttemptState.Input);
            sut.CanInput.Should().BeTrue();
            sut.CanSubmit.Should().BeFalse();
            sut.DigitPlace.Should().Be(1);
            sut.Inputs[0].Should().Be(input0);
            sut.Inputs[1].Should().BeNull();
            sut.Inputs[2].Should().BeNull();
            sut.Inputs[3].Should().BeNull();
        }

        [Fact]
        public void When_back_called_after_last_setinput_then_setproperties()
        {
            var digitCount = 4;
            var input0 = '0';
            var input1 = '1';
            var input2 = '2';
            var input3 = '3';

            var sut = new AttemptTracker();

            sut.Start(digitCount);
            sut.SetInput(input0);
            sut.SetInput(input1);
            sut.SetInput(input2);
            sut.SetInput(input3);
            sut.Back();

            sut.State.Should().Be(AttemptState.Input);
            sut.CanInput.Should().BeTrue();
            sut.CanSubmit.Should().BeFalse();
            sut.DigitPlace.Should().Be(digitCount - 1);
            sut.Inputs[0].Should().Be(input0);
            sut.Inputs[1].Should().Be(input1);
            sut.Inputs[2].Should().Be(input2);
            sut.Inputs[3].Should().BeNull();
        }
    }
}
