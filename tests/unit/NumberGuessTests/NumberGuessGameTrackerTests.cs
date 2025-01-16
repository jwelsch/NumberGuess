using FluentAssertions;
using NumberGuess;

namespace NumberGuessTests
{
    public class NumberGuessGameTrackerTests
    {
        [Fact]
        public void When_constructor_called_then_properties_set()
        {
            var sut = new NumberGuessGameTracker();

            sut.Answer.Should().BeEmpty();
            sut.AttemptCount.Should().Be(0);
            sut.AttemptResults.Should().BeEmpty();
            sut.AttemptsRemaining.Should().Be(0);
            sut.CanInput.Should().BeFalse();
            sut.CanSubmit.Should().BeFalse();
            sut.DigitPlace.Should().Be(-1);
            sut.IsComplete.Should().BeFalse();
            sut.IsPlaying.Should().BeFalse();
            sut.State.Should().Be(NumberGuessGameState.Uninitialized);
        }

        [Fact]
        public void When_start_called_with_valid_arguments_then_properties_set()
        {
            var answer = new char[] { '1', '2', '3', '4' };
            var attemptCount = 4;

            var sut = new NumberGuessGameTracker();

            sut.Start(answer, attemptCount);

            sut.Answer.Should().BeEquivalentTo(answer);
            sut.AttemptCount.Should().Be(attemptCount);
            sut.AttemptResults.Should().BeEmpty();
            sut.AttemptsRemaining.Should().Be(attemptCount);
            sut.CanInput.Should().BeTrue();
            sut.CanSubmit.Should().BeFalse();
            sut.DigitPlace.Should().Be(0);
            sut.IsComplete.Should().BeFalse();
            sut.IsPlaying.Should().BeTrue();
            sut.State.Should().Be(NumberGuessGameState.Playing);
        }

        [Fact]
        public void When_start_called_with_empty_answer_then_throw_argumentexception()
        {
            var answer = new char[0];
            var attemptCount = 4;

            var sut = new NumberGuessGameTracker();

            Action action = () => sut.Start(answer, attemptCount);

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void When_start_called_with_answer_too_long_then_throw_argumentexception()
        {
            var answer = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', 'A' };
            var attemptCount = 4;

            var sut = new NumberGuessGameTracker();

            Action action = () => sut.Start(answer, attemptCount);

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void When_start_called_with_attemptcount_zero_then_throw_argumentexception()
        {
            var answer = new char[] { '1', '2', '3', '4' };
            var attemptCount = 0;

            var sut = new NumberGuessGameTracker();

            Action action = () => sut.Start(answer, attemptCount);

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void When_start_called_with_attemptcount_negative_then_throw_argumentexception()
        {
            var answer = new char[] { '1', '2', '3', '4' };
            var attemptCount = -1;

            var sut = new NumberGuessGameTracker();

            Action action = () => sut.Start(answer, attemptCount);

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void When_start_called_with_attemptcount_greater_than_max_then_throw_argumentexception()
        {
            var answer = new char[] { '1', '2', '3', '4' };
            var attemptCount = NumberGuessGameTracker.AttemptCountMax + 1;

            var sut = new NumberGuessGameTracker();

            Action action = () => sut.Start(answer, attemptCount);

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void When_first_input_called_then_properties_set()
        {
            var answer = new char[] { '1', '2', '3', '4' };
            var attemptCount = 4;
            var input0 = '0';

            var sut = new NumberGuessGameTracker();

            sut.Start(answer, attemptCount);
            sut.Input(input0);

            sut.Answer.Should().BeEquivalentTo(answer);
            sut.AttemptCount.Should().Be(attemptCount);
            sut.AttemptResults.Should().BeEmpty();
            sut.AttemptsRemaining.Should().Be(attemptCount);
            sut.CanInput.Should().BeTrue();
            sut.CanSubmit.Should().BeFalse();
            sut.DigitPlace.Should().Be(1);
            sut.IsComplete.Should().BeFalse();
            sut.IsPlaying.Should().BeTrue();
            sut.State.Should().Be(NumberGuessGameState.Playing);
        }

        [Fact]
        public void When_last_input_called_then_properties_set()
        {
            var answer = new char[] { '1', '2', '3', '4' };
            var attemptCount = 4;
            var input0 = '0';
            var input1 = '1';
            var input2 = '2';
            var input3 = '3';

            var sut = new NumberGuessGameTracker();

            sut.Start(answer, attemptCount);
            sut.Input(input0);
            sut.Input(input1);
            sut.Input(input2);
            sut.Input(input3);

            sut.Answer.Should().BeEquivalentTo(answer);
            sut.AttemptCount.Should().Be(attemptCount);
            sut.AttemptResults.Should().BeEmpty();
            sut.AttemptsRemaining.Should().Be(attemptCount);
            sut.CanInput.Should().BeFalse();
            sut.CanSubmit.Should().BeTrue();
            sut.DigitPlace.Should().Be(4);
            sut.IsComplete.Should().BeFalse();
            sut.IsPlaying.Should().BeTrue();
            sut.State.Should().Be(NumberGuessGameState.Playing);
        }

        [Fact]
        public void When_start_called_before_calling_start_then_throw_invalidoperationexception()
        {
            var input0 = '0';

            var sut = new NumberGuessGameTracker();

            Action action = () => sut.Input(input0);

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void When_back_called_before_calling_start_then_throw_invalidoperationexception()
        {
            var sut = new NumberGuessGameTracker();

            Action action = () => sut.Back();

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void When_back_is_called_before_first_input_then_properties_set()
        {
            var answer = new char[] { '1', '2', '3', '4' };
            var attemptCount = 4;

            var sut = new NumberGuessGameTracker();

            sut.Start(answer, attemptCount);
            sut.Back();

            sut.Answer.Should().BeEquivalentTo(answer);
            sut.AttemptCount.Should().Be(attemptCount);
            sut.AttemptResults.Should().BeEmpty();
            sut.AttemptsRemaining.Should().Be(attemptCount);
            sut.CanInput.Should().BeTrue();
            sut.CanSubmit.Should().BeFalse();
            sut.DigitPlace.Should().Be(0);
            sut.IsComplete.Should().BeFalse();
            sut.IsPlaying.Should().BeTrue();
            sut.State.Should().Be(NumberGuessGameState.Playing);
        }

        [Fact]
        public void When_back_called_after_first_input_then_properties_set()
        {
            var answer = new char[] { '1', '2', '3', '4' };
            var attemptCount = 4;
            var input0 = '0';

            var sut = new NumberGuessGameTracker();

            sut.Start(answer, attemptCount);
            sut.Input(input0);
            sut.Back();

            sut.Answer.Should().BeEquivalentTo(answer);
            sut.AttemptCount.Should().Be(attemptCount);
            sut.AttemptResults.Should().BeEmpty();
            sut.AttemptsRemaining.Should().Be(attemptCount);
            sut.CanInput.Should().BeTrue();
            sut.CanSubmit.Should().BeFalse();
            sut.DigitPlace.Should().Be(0);
            sut.IsComplete.Should().BeFalse();
            sut.IsPlaying.Should().BeTrue();
            sut.State.Should().Be(NumberGuessGameState.Playing);
        }

        [Fact]
        public void When_back_called_after_last_input_then_properties_set()
        {
            var answer = new char[] { '1', '2', '3', '4' };
            var attemptCount = 4;
            var input0 = '0';
            var input1 = '1';
            var input2 = '2';
            var input3 = '3';

            var sut = new NumberGuessGameTracker();

            sut.Start(answer, attemptCount);
            sut.Input(input0);
            sut.Input(input1);
            sut.Input(input2);
            sut.Input(input3);
            sut.Back();

            sut.Answer.Should().BeEquivalentTo(answer);
            sut.AttemptCount.Should().Be(attemptCount);
            sut.AttemptResults.Should().BeEmpty();
            sut.AttemptsRemaining.Should().Be(attemptCount);
            sut.CanInput.Should().BeTrue();
            sut.CanSubmit.Should().BeFalse();
            sut.DigitPlace.Should().Be(3);
            sut.IsComplete.Should().BeFalse();
            sut.IsPlaying.Should().BeTrue();
            sut.State.Should().Be(NumberGuessGameState.Playing);
        }

        [Fact]
        public void When_submit_called_before_start_then_throw_invalidoperationexception()
        {
            var sut = new NumberGuessGameTracker();

            Action action = () => sut.Submit();

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void When_submit_called_before_all_digits_input_then_throw_invalidoperationexception()
        {
            var answer = new char[] { '1', '2', '3', '4' };
            var attemptCount = 4;
            var input0 = '0';
            var input1 = '1';
            var input2 = '2';

            var sut = new NumberGuessGameTracker();

            sut.Start(answer, attemptCount);
            sut.Input(input0);
            sut.Input(input1);
            sut.Input(input2);

            Action action = () => sut.Submit();

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void When_submit_called_after_last_input_and_inputs_have_wrong_digit_and_wrong_placement_and_correct_then_properties_set()
        {
            var answer = new char[] { '1', '2', '3', '4' };
            var attemptCount = 4;
            var input0 = '0';
            var input1 = '1';
            var input2 = '2';
            var input3 = '4';
            var expectedAttemptResult = new AttemptResult[]
            {
                new AttemptResult(AttemptState.Lost,
                    new DigitInputResult[]
                    {
                        new DigitInputResult(DigitInputState.WrongDigit, input0),
                        new DigitInputResult(DigitInputState.WrongPlacement, input1),
                        new DigitInputResult(DigitInputState.WrongPlacement, input2),
                        new DigitInputResult(DigitInputState.Correct, input3)
                    })
            };

            var sut = new NumberGuessGameTracker();

            sut.Start(answer, attemptCount);
            sut.Input(input0);
            sut.Input(input1);
            sut.Input(input2);
            sut.Input(input3);
            sut.Submit();

            sut.Answer.Should().BeEquivalentTo(answer);
            sut.AttemptCount.Should().Be(attemptCount);
            sut.AttemptResults.Should().BeEquivalentTo(expectedAttemptResult);
            sut.AttemptsRemaining.Should().Be(attemptCount - 1);
            sut.CanInput.Should().BeTrue();
            sut.CanSubmit.Should().BeFalse();
            sut.DigitPlace.Should().Be(0);
            sut.IsComplete.Should().BeFalse();
            sut.IsPlaying.Should().BeTrue();
            sut.State.Should().Be(NumberGuessGameState.Playing);
        }

        [Fact]
        public void When_submit_called_after_last_input_and_inputs_have_all_wrong_digit_then_properties_set()
        {
            var answer = new char[] { '1', '2', '3', '4' };
            var attemptCount = 4;
            var input0 = '9';
            var input1 = '8';
            var input2 = '7';
            var input3 = '6';
            var expectedAttemptResult = new AttemptResult[]
            {
                new AttemptResult(AttemptState.Lost,
                    new DigitInputResult[]
                    {
                        new DigitInputResult(DigitInputState.WrongDigit, input0),
                        new DigitInputResult(DigitInputState.WrongDigit, input1),
                        new DigitInputResult(DigitInputState.WrongDigit, input2),
                        new DigitInputResult(DigitInputState.WrongDigit, input3)
                    })
            };

            var sut = new NumberGuessGameTracker();

            sut.Start(answer, attemptCount);
            sut.Input(input0);
            sut.Input(input1);
            sut.Input(input2);
            sut.Input(input3);
            sut.Submit();

            sut.Answer.Should().BeEquivalentTo(answer);
            sut.AttemptCount.Should().Be(attemptCount);
            sut.AttemptResults.Should().BeEquivalentTo(expectedAttemptResult);
            sut.AttemptsRemaining.Should().Be(attemptCount - 1);
            sut.CanInput.Should().BeTrue();
            sut.CanSubmit.Should().BeFalse();
            sut.DigitPlace.Should().Be(0);
            sut.IsComplete.Should().BeFalse();
            sut.IsPlaying.Should().BeTrue();
            sut.State.Should().Be(NumberGuessGameState.Playing);
        }

        [Fact]
        public void When_submit_called_after_last_input_and_inputs_have_all_wrong_placement_then_properties_set()
        {
            var answer = new char[] { '1', '2', '3', '4' };
            var attemptCount = 4;
            var input0 = '4';
            var input1 = '3';
            var input2 = '2';
            var input3 = '1';
            var expectedAttemptResult = new AttemptResult[]
            {
                new AttemptResult(AttemptState.Lost,
                    new DigitInputResult[]
                    {
                        new DigitInputResult(DigitInputState.WrongPlacement, input0),
                        new DigitInputResult(DigitInputState.WrongPlacement, input1),
                        new DigitInputResult(DigitInputState.WrongPlacement, input2),
                        new DigitInputResult(DigitInputState.WrongPlacement, input3)
                    })
            };

            var sut = new NumberGuessGameTracker();

            sut.Start(answer, attemptCount);
            sut.Input(input0);
            sut.Input(input1);
            sut.Input(input2);
            sut.Input(input3);
            sut.Submit();

            sut.Answer.Should().BeEquivalentTo(answer);
            sut.AttemptCount.Should().Be(attemptCount);
            sut.AttemptResults.Should().BeEquivalentTo(expectedAttemptResult);
            sut.AttemptsRemaining.Should().Be(attemptCount - 1);
            sut.CanInput.Should().BeTrue();
            sut.CanSubmit.Should().BeFalse();
            sut.DigitPlace.Should().Be(0);
            sut.IsComplete.Should().BeFalse();
            sut.IsPlaying.Should().BeTrue();
            sut.State.Should().Be(NumberGuessGameState.Playing);
        }

        [Fact]
        public void When_submit_called_after_last_input_and_inputs_have_all_correct_then_properties_set()
        {
            var answer = new char[] { '1', '2', '3', '4' };
            var attemptCount = 4;
            var input0 = '1';
            var input1 = '2';
            var input2 = '3';
            var input3 = '4';
            var expectedAttemptResult = new AttemptResult[]
            {
                new AttemptResult(AttemptState.Won,
                    new DigitInputResult[]
                    {
                        new DigitInputResult(DigitInputState.Correct, input0),
                        new DigitInputResult(DigitInputState.Correct, input1),
                        new DigitInputResult(DigitInputState.Correct, input2),
                        new DigitInputResult(DigitInputState.Correct, input3)
                    })
            };

            var sut = new NumberGuessGameTracker();

            sut.Start(answer, attemptCount);
            sut.Input(input0);
            sut.Input(input1);
            sut.Input(input2);
            sut.Input(input3);
            sut.Submit();

            sut.Answer.Should().BeEquivalentTo(answer);
            sut.AttemptCount.Should().Be(attemptCount);
            sut.AttemptResults.Should().BeEquivalentTo(expectedAttemptResult);
            sut.AttemptsRemaining.Should().Be(attemptCount - 1);
            sut.CanInput.Should().BeFalse();
            sut.CanSubmit.Should().BeFalse();
            sut.DigitPlace.Should().Be(4);
            sut.IsComplete.Should().BeTrue();
            sut.IsPlaying.Should().BeFalse();
            sut.State.Should().Be(NumberGuessGameState.Won);
        }

        [Fact]
        public void When_on_last_attempt_submit_called_after_last_input_and_inputs_have_all_correct_then_properties_set()
        {
            var answer = new char[] { '1', '2', '3', '4' };
            var attemptCount = 4;
            var input0 = new char[] { '0', '1', '2', '3' };
            var input1 = new char[] { '9', '8', '7', '6' };
            var input2 = new char[] { '5', '4', '3', '2' };
            var input3 = new char[] { '1', '2', '3', '4' };
            var expectedAttemptResult = new AttemptResult[]
            {
                new AttemptResult(AttemptState.Lost,
                    new DigitInputResult[]
                    {
                        new DigitInputResult(DigitInputState.WrongDigit, input0[0]),
                        new DigitInputResult(DigitInputState.WrongPlacement, input0[1]),
                        new DigitInputResult(DigitInputState.WrongPlacement, input0[2]),
                        new DigitInputResult(DigitInputState.WrongPlacement, input0[3])
                    }),
                new AttemptResult(AttemptState.Lost,
                    new DigitInputResult[]
                    {
                        new DigitInputResult(DigitInputState.WrongDigit, input1[0]),
                        new DigitInputResult(DigitInputState.WrongDigit, input1[1]),
                        new DigitInputResult(DigitInputState.WrongDigit, input1[2]),
                        new DigitInputResult(DigitInputState.WrongDigit, input1[3])
                    }),
                new AttemptResult(AttemptState.Lost,
                    new DigitInputResult[]
                    {
                        new DigitInputResult(DigitInputState.WrongDigit, input2[0]),
                        new DigitInputResult(DigitInputState.WrongPlacement, input2[1]),
                        new DigitInputResult(DigitInputState.Correct, input2[2]),
                        new DigitInputResult(DigitInputState.WrongPlacement, input2[3])
                    }),
                new AttemptResult(AttemptState.Won,
                    new DigitInputResult[]
                    {
                        new DigitInputResult(DigitInputState.Correct, input3[0]),
                        new DigitInputResult(DigitInputState.Correct, input3[1]),
                        new DigitInputResult(DigitInputState.Correct, input3[2]),
                        new DigitInputResult(DigitInputState.Correct, input3[3])
                    }),
            };

            var sut = new NumberGuessGameTracker();

            sut.Start(answer, attemptCount);
            sut.Input(input0[0]);
            sut.Input(input0[1]);
            sut.Input(input0[2]);
            sut.Input(input0[3]);
            sut.Submit();
            sut.Input(input1[0]);
            sut.Input(input1[1]);
            sut.Input(input1[2]);
            sut.Input(input1[3]);
            sut.Submit();
            sut.Input(input2[0]);
            sut.Input(input2[1]);
            sut.Input(input2[2]);
            sut.Input(input2[3]);
            sut.Submit();
            sut.Input(input3[0]);
            sut.Input(input3[1]);
            sut.Input(input3[2]);
            sut.Input(input3[3]);
            sut.Submit();

            sut.Answer.Should().BeEquivalentTo(answer);
            sut.AttemptCount.Should().Be(attemptCount);
            sut.AttemptResults.Should().BeEquivalentTo(expectedAttemptResult);
            //CompareAttemptResults(sut.AttemptResults, expectedAttemptResult);
            sut.AttemptsRemaining.Should().Be(attemptCount - 4);
            sut.CanInput.Should().BeFalse();
            sut.CanSubmit.Should().BeFalse();
            sut.DigitPlace.Should().Be(4);
            sut.IsComplete.Should().BeTrue();
            sut.IsPlaying.Should().BeFalse();
            sut.State.Should().Be(NumberGuessGameState.Won);
        }

        [Fact]
        public void When_on_last_attempt_submit_called_after_last_input_and_inputs_have_wrong_digit_and_wrong_placement_then_properties_set()
        {
            var answer = new char[] { '1', '2', '3', '4' };
            var attemptCount = 4;
            var input0 = new char[] { '0', '1', '2', '3' };
            var input1 = new char[] { '9', '8', '7', '6' };
            var input2 = new char[] { '5', '4', '3', '2' };
            var input3 = new char[] { '9', '2', '3', '4' };
            var expectedAttemptResult = new AttemptResult[]
            {
                new AttemptResult(AttemptState.Lost,
                    new DigitInputResult[]
                    {
                        new DigitInputResult(DigitInputState.WrongDigit, input0[0]),
                        new DigitInputResult(DigitInputState.WrongPlacement, input0[1]),
                        new DigitInputResult(DigitInputState.WrongPlacement, input0[2]),
                        new DigitInputResult(DigitInputState.WrongPlacement, input0[3])
                    }),
                new AttemptResult(AttemptState.Lost,
                    new DigitInputResult[]
                    {
                        new DigitInputResult(DigitInputState.WrongDigit, input1[0]),
                        new DigitInputResult(DigitInputState.WrongDigit, input1[1]),
                        new DigitInputResult(DigitInputState.WrongDigit, input1[2]),
                        new DigitInputResult(DigitInputState.WrongDigit, input1[3])
                    }),
                new AttemptResult(AttemptState.Lost,
                    new DigitInputResult[]
                    {
                        new DigitInputResult(DigitInputState.WrongDigit, input2[0]),
                        new DigitInputResult(DigitInputState.WrongPlacement, input2[1]),
                        new DigitInputResult(DigitInputState.Correct, input2[2]),
                        new DigitInputResult(DigitInputState.WrongPlacement, input2[3])
                    }),
                new AttemptResult(AttemptState.Lost,
                    new DigitInputResult[]
                    {
                        new DigitInputResult(DigitInputState.WrongDigit, input3[0]),
                        new DigitInputResult(DigitInputState.Correct, input3[1]),
                        new DigitInputResult(DigitInputState.Correct, input3[2]),
                        new DigitInputResult(DigitInputState.Correct, input3[3])
                    }),
            };

            var sut = new NumberGuessGameTracker();

            sut.Start(answer, attemptCount);
            sut.Input(input0[0]);
            sut.Input(input0[1]);
            sut.Input(input0[2]);
            sut.Input(input0[3]);
            sut.Submit();
            sut.Input(input1[0]);
            sut.Input(input1[1]);
            sut.Input(input1[2]);
            sut.Input(input1[3]);
            sut.Submit();
            sut.Input(input2[0]);
            sut.Input(input2[1]);
            sut.Input(input2[2]);
            sut.Input(input2[3]);
            sut.Submit();
            sut.Input(input3[0]);
            sut.Input(input3[1]);
            sut.Input(input3[2]);
            sut.Input(input3[3]);
            sut.Submit();

            sut.Answer.Should().BeEquivalentTo(answer);
            sut.AttemptCount.Should().Be(attemptCount);
            sut.AttemptResults.Should().BeEquivalentTo(expectedAttemptResult);
            //CompareAttemptResults(sut.AttemptResults, expectedAttemptResult);
            sut.AttemptsRemaining.Should().Be(attemptCount - 4);
            sut.CanInput.Should().BeFalse();
            sut.CanSubmit.Should().BeFalse();
            sut.DigitPlace.Should().Be(4);
            sut.IsComplete.Should().BeTrue();
            sut.IsPlaying.Should().BeFalse();
            sut.State.Should().Be(NumberGuessGameState.Lost);
        }

        //private static void CompareAttemptResults(IEnumerable<AttemptResult> actual, IEnumerable<AttemptResult> expected)
        //{
        //    var actualCount = 0;
        //    var expectedCount = 0;

        //    var actualEnumerator = actual.GetEnumerator();
        //    var expectedEnumerator = expected.GetEnumerator();

        //    while (expectedEnumerator.MoveNext())
        //    {
        //        if (!actualEnumerator.MoveNext())
        //        {
        //            break;
        //        }

        //        actualCount++;
        //        expectedCount++;

        //        actualEnumerator.Current.State.Should().Be(expectedEnumerator.Current.State);

        //        var actualDigitCount = 0;
        //        var expectedDigitCount = 0;

        //        var actualDigitEnumerator = actualEnumerator.Current.DigitInputResult.GetEnumerator();
        //        var expectedDigitEnumerator = expectedEnumerator.Current.DigitInputResult.GetEnumerator();

        //        while (expectedDigitEnumerator.MoveNext())
        //        {
        //            if (!actualDigitEnumerator.MoveNext())
        //            {
        //                break;
        //            }

        //            actualDigitCount++;
        //            expectedDigitCount++;

        //            actualDigitEnumerator.Current.Input.Should().Be(expectedDigitEnumerator.Current.Input);
        //            actualDigitEnumerator.Current.State.Should().Be(expectedDigitEnumerator.Current.State);
        //        }

        //        while (actualDigitEnumerator.MoveNext())
        //        {
        //            actualDigitCount++;
        //        }

        //        actualDigitCount.Should().Be(expectedDigitCount);
        //    }

        //    while (actualEnumerator.MoveNext())
        //    {
        //        actualCount++;
        //    }

        //    actualCount.Should().Be(expectedCount);
        //}
    }
}
