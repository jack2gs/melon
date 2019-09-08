using Com.Melon.Core.Infrastructure;
using FluentAssertions;
using System;
using Xunit;
using XunitExtensions;

namespace Com.Melon.Core.Unit.Test.Infrastructure
{
    public class TestAssertStateTrueBase : Specification
    {
        protected bool Value;

        protected string Message;

        protected Exception ActualException;

        protected override void EstablishContext()
        {
            Message = "The state should be true.";
        }

        protected override void Because()
        {
            ActualException = Record.Exception(() =>
            {
                AssertionConcern.AssertStateTrue(Value, Message);
            });
        }
    }

    public class When_value_is_true_but_should_be_true : TestAssertStateTrueBase
    {
        protected override void EstablishContext()
        {
            Value = true;
            base.EstablishContext();
        }

        [Observation]
        void should_not_throw_exception()
        {
            ActualException.Should().BeNull();
        }
    }

    public class When_value_is_false_but_should_be_true  : TestAssertStateTrueBase
    {
        protected override void EstablishContext()
        {
            Value = false;
            base.EstablishContext();
        }

        [Observation]
        void should_throw_exception()
        {
            ActualException.Should().NotBeNull();
            ActualException.Should().BeOfType<InvalidOperationException>();
        }

        [Observation]
        void shoul_have_exception_message()
        {
            ActualException.Message.Should().Be(Message);
        }
    }

    public class TestAssertStateFalseBase : Specification
    {
        protected bool Value;

        protected string Message;

        protected Exception ActualException;

        protected override void EstablishContext()
        {
            Message = "The state should be false.";
        }

        protected override void Because()
        {
            ActualException = Record.Exception(() =>
            {
                AssertionConcern.AssertStateFalse(Value, Message);
            });
        }
    }

    public class When_value_is_false_but_should_be_false : TestAssertStateFalseBase
    {
        protected override void EstablishContext()
        {
            Value = false;
            base.EstablishContext();
        }

        [Observation]
        void should_not_throw_exception()
        {
            ActualException.Should().BeNull();
        }
    }

    public class When_value_is_true_but_should_be_false : TestAssertStateFalseBase
    {
        protected override void EstablishContext()
        {
            Value = true;
            base.EstablishContext();
        }

        [Observation]
        void should_throw_exception()
        {
            ActualException.Should().NotBeNull();
            ActualException.Should().BeOfType<InvalidOperationException>();
        }

        [Observation]
        void shoul_have_exception_message()
        {
            ActualException.Message.Should().Be(Message);
        }
    }

    public class TestAssertArgumentTrueBase: Specification
    {
        protected bool Value;

        protected string Message;

        protected Exception ActualException;

        protected override void EstablishContext()
        {
            Message = "The value should be true.";
        }

        protected override void Because()
        {
            ActualException = Record.Exception(() =>
            {
                AssertionConcern.AssertArgumentTrue(Value, Message);
            });
        }
    }

    public class When_value_is_true: TestAssertArgumentTrueBase
    {
        protected override void EstablishContext()
        {
            Value = true;
            base.EstablishContext();
        }

        [Observation]
        void should_not_throw_exception()
        {
            ActualException.Should().BeNull();
        }
    }

    public class When_value_is_false : TestAssertArgumentTrueBase
    {
        protected override void EstablishContext()
        {
            Value = false;
            base.EstablishContext();
        }

        [Observation]
        void should_throw_exception()
        {
            ActualException.Should().NotBeNull();
            ActualException.Should().BeOfType<ArgumentException>();
        }

        [Observation]
        void shoul_have_exception_message()
        {
            ActualException.Message.Should().Be(Message);
        }
    }

    public class AssertArgumentRangeForLongType : Specification
    {
        protected long Argument;

        protected long Minimum, Maximum;

        protected Exception ActualException;

        protected string ExpectedMessage;

        protected override void EstablishContext()
        {
            ExpectedMessage = $"The value should be between {Minimum} and {Maximum}.";
        }

        protected override void Because()
        {
            ActualException = Record.Exception(() =>
            {
                AssertionConcern.AssertArgumentRange(Argument, Minimum, Maximum, ExpectedMessage);
            });
        }
    }

    public class When_the_long_value_is_in_range : AssertArgumentRangeForLongType
    {
        protected override void EstablishContext()
        {
            Argument = 10;
            Minimum = 5;
            Maximum = 10;
            base.EstablishContext();
        }

        [Observation]
        void should_not_throw_exception()
        {
            ActualException.Should().BeNull();
        }
    }

    public class When_the_long_value_out_of_range : AssertArgumentRangeForLongType
    {
        protected override void EstablishContext()
        {
            Argument = 0;
            Minimum = 1;
            Maximum = 5;
            base.EstablishContext();
        }

        [Observation]
        void should_throw_exception()
        {
            ActualException.Should().NotBeNull();
            ActualException.Should().BeOfType<ArgumentException>();
        }

        [Observation]
        void shoul_have_exception_message()
        {
            ActualException.Message.Should().Be(ExpectedMessage);
        }
    }

    public class AssertArgumentRangeForIntType : Specification
    {
        protected int Argument;

        protected int Minimum, Maximum;

        protected Exception ActualException;

        protected string ExpectedMessage;

        protected override void EstablishContext()
        {
            ExpectedMessage = $"The value should be between {Minimum} and {Maximum}.";
        }

        protected override void Because()
        {
            ActualException = Record.Exception(() =>
            {
                AssertionConcern.AssertArgumentRange(Argument, Minimum, Maximum, ExpectedMessage);
            });
        }
    }

    public class When_the_int_value_is_in_range : AssertArgumentRangeForIntType
    {
        protected override void EstablishContext()
        {
            Argument = 10;
            Minimum = 5;
            Maximum = 10;
            base.EstablishContext();
        }

        [Observation]
        void should_not_throw_exception()
        {
            ActualException.Should().BeNull();
        }
    }

    public class When_the_int_value_out_of_range : AssertArgumentRangeForIntType
    {
        protected override void EstablishContext()
        {
            Argument = 0;
            Minimum = 1;
            Maximum = 5;
            base.EstablishContext();
        }

        [Observation]
        void should_throw_exception()
        {
            ActualException.Should().NotBeNull();
            ActualException.Should().BeOfType<ArgumentException>();
        }

        [Observation]
        void shoul_have_exception_message()
        {
            ActualException.Message.Should().Be(ExpectedMessage);
        }
    }

    public class AssertArgumentRangeForFloatType : Specification
    {
        protected float Argument;

        protected float Minimum, Maximum;

        protected Exception ActualException;

        protected string ExpectedMessage;

        protected override void EstablishContext()
        {
            ExpectedMessage = $"The value should be between {Minimum} and {Maximum}.";
        }

        protected override void Because()
        {
            ActualException = Record.Exception(() =>
            {
                AssertionConcern.AssertArgumentRange(Argument, Minimum, Maximum, ExpectedMessage);
            });
        }
    }

    public class When_the_float_value_is_in_range : AssertArgumentRangeForFloatType
    {
        protected override void EstablishContext()
        {
            Argument = 10.14f;
            Minimum = 5.12f;
            Maximum = 10.68f;
            base.EstablishContext();
        }

        [Observation]
        void should_not_throw_exception()
        {
            ActualException.Should().BeNull();
        }
    }

    public class When_the_float_value_out_of_range : AssertArgumentRangeForFloatType
    {
        protected override void EstablishContext()
        {
            Argument = 1.05f;
            Minimum = 5.12f;
            Maximum = 10.68f;
            base.EstablishContext();
        }

        [Observation]
        void should_throw_exception()
        {
            ActualException.Should().NotBeNull();
            ActualException.Should().BeOfType<ArgumentException>();
        }

        [Observation]
        void shoul_have_exception_message()
        {
            ActualException.Message.Should().Be(ExpectedMessage);
        }
    }

    public class AssertArgumentRangeForDoubleType : Specification
    {
        protected double Argument;

        protected double Minimum, Maximum;

        protected Exception ActualException;

        protected string ExpectedMessage;

        protected override void EstablishContext()
        {
            ExpectedMessage = $"The value should be between {Minimum} and {Maximum}.";
        }

        protected override void Because()
        {
            ActualException = Record.Exception(() =>
            {
                AssertionConcern.AssertArgumentRange(Argument, Minimum, Maximum, ExpectedMessage);
            });
        }
    }

    public class When_the_double_value_is_in_range: AssertArgumentRangeForDoubleType
    {
        protected override void EstablishContext()
        {
            Argument = 10.14;
            Minimum = 5.12;
            Maximum = 10.68;
            base.EstablishContext();
        }

        [Observation]
        void should_not_throw_exception()
        {
            ActualException.Should().BeNull();
        }
    }

    public class When_the_double_value_out_of_range : AssertArgumentRangeForDoubleType
    {
        protected override void EstablishContext()
        {
            Argument = 1.05;
            Minimum = 5.12;
            Maximum = 10.68;
            base.EstablishContext();
        }

        [Observation]
        void should_throw_exception()
        {
            ActualException.Should().NotBeNull();
            ActualException.Should().BeOfType<ArgumentException>();
        }

        [Observation]
        void shoul_have_exception_message()
        {
            ActualException.Message.Should().Be(ExpectedMessage);
        }
    }

    public class TestAssertArgumentNotNullBase: Specification
    {
        protected string Argument;

        protected Exception ActualException;

        protected string ExpectedMessage;

        protected override void EstablishContext()
        {
            ExpectedMessage = $"The value should not be null.";
        }

        protected override void Because()
        {
            ActualException = Record.Exception(() =>
            {
                AssertionConcern.AssertArgumentNotNull(Argument, ExpectedMessage);
            });
        }
    }

    public class When_the_argument_is_null: TestAssertArgumentNotNullBase
    {
        protected override void EstablishContext()
        {
            Argument = null;
            base.EstablishContext();
        }

        [Observation]
        void should_throw_exception()
        {
            ActualException.Should().NotBeNull();
            ActualException.Should().BeOfType<ArgumentException>();
        }

        [Observation]
        void should_has_exception_message()
        {
            ActualException.Message.Should().Be(ExpectedMessage);
        }
    }

    public class When_the_argument_is_not_null : TestAssertArgumentNotNullBase
    {
        protected override void EstablishContext()
        {
            Argument = "test";
            base.EstablishContext();
        }

        [Observation]
        void should_not_throw_exception()
        {
            ActualException.Should().BeNull();
        }
    }

    public class TestAssertArgumentNotEqualsBase: Specification
    {
        protected object Argument1;

        protected object Argument2;

        protected string Message;

        protected Exception ActualException;

        protected override void Because()
        {
            ActualException = Record.Exception(() =>
            {
                AssertionConcern.AssertArgumentNotEquals(Argument1, Argument2, Message);
            });
        }
    }

    public class When_objects_are_equal: TestAssertArgumentNotEqualsBase
    {
        protected override void EstablishContext()
        {
            Argument1 = 1;
            Argument2 = 1;
            Message = "Should not equal.";
        }

        [Observation]
        void should_throw_exception()
        {
            ActualException.Should().NotBeNull();
        }

        [Observation]
        void should_has_exception_message()
        {
            ActualException.Message.Should().Be(Message);
        }
    }

    public class When_objects_are_not_equal : TestAssertArgumentNotEqualsBase
    {
        protected override void EstablishContext()
        {
            Argument1 = 1;
            Argument2 = 2;
            Message = "Should not equal.";
        }

        [Observation]
        void should_not_throw_exception()
        {
            ActualException.Should().BeNull();
        }
    }

    public class TestAssertArgumentNotEmptyBase: Specification
    {
        protected string StringValue;

        protected string Message;

        protected Exception ActualException;

        protected override void Because()
        {
            ActualException = Record.Exception(() =>
            {
                AssertionConcern.AssertArgumentNotEmpty(StringValue, Message);
            });
        }
    }

    public class When_argument_is_a_valid_string: TestAssertArgumentNotEmptyBase
    {
        protected override void EstablishContext()
        {
            StringValue = "abc";
        }

        [Observation]
        void should_not_throw_exception()
        {
            ActualException.Should().BeNull();
        }
    }

    public class When_argument_is_null: TestAssertArgumentNotEmptyBase
    {
        protected override void EstablishContext()
        {
            StringValue = null;
            Message = "Should not be empty";
        }

        [Observation]
        void shoud_throw_exception()
        {
            ActualException.Should().NotBeNull();
            ActualException.Should().BeOfType<ArgumentException>();
        }

        [Observation]
        void should_has_exception_message()
        {
            ActualException.Message.Should().Be(Message);
        }
    }

    public class When_argument_is_empty : TestAssertArgumentNotEmptyBase
    {
        protected override void EstablishContext()
        {
            StringValue = string.Empty;
            Message = "Should not be empty";
        }

        [Observation]
        void shoud_throw_exception()
        {
            ActualException.Should().NotBeNull();
            ActualException.Should().BeOfType<ArgumentException>();
        }

        [Observation]
        void should_has_exception_message()
        {
            ActualException.Message.Should().Be(Message);
        }
    }

    public class TestAssertArgumentMatchesBase: Specification
    {
        protected string Pattern;

        protected string StringValue;

        protected string ExpectedMessage;

        protected Exception ActualException;

        protected override void Because()
        {
            ActualException = Record.Exception(() =>
            {
                AssertionConcern.AssertArgumentMatches(Pattern, StringValue, ExpectedMessage);
            }); 
        }
    }

    public class When_match: TestAssertArgumentMatchesBase
    {
        protected override void EstablishContext()
        {
            Pattern = "^[123]+$";
            StringValue = "12223";
        }

        [Observation]
        void should_not_throw_exception()
        {
            ActualException.Should().BeNull();
        }
    }

    public class When_not_match : TestAssertArgumentMatchesBase
    {
        protected override void EstablishContext()
        {
            Pattern = "^[123]+$";
            StringValue = "abc12223";
            ExpectedMessage = "The value should be numberic.";
        }

        [Observation]
        void should_throw_exception()
        {
            ActualException.Should().NotBeNull();
            ActualException.Should().BeOfType<ArgumentException>();
        }

        [Observation]
        void should_has_exception_message()
        {
            ActualException.Message.Should().Be(ExpectedMessage);
        }
    }

    public class TestAssertArgumentLengthRangeBase : Specification
    {
        protected string Argument;

        protected Exception ActualException;

        protected string ExpectedMessage;

        protected int MaximumLength;

        protected int MinimumLength;

        protected override void EstablishContext()
        {
            ExpectedMessage = $"The length should between {MinimumLength} and {MaximumLength}.";
        }

        protected override void Because()
        {
            ActualException = Record.Exception(() =>
            {
                AssertionConcern.AssertArgumentLength(Argument, MinimumLength, MaximumLength, ExpectedMessage);
            });
        }
    }

    public class When_the_length_is_in_range: TestAssertArgumentLengthRangeBase
    {
        protected override void EstablishContext()
        {
            MaximumLength = 10;
            MinimumLength = 5;
            Argument = "hello";
            base.EstablishContext();
        }

        [Observation]
        void should_not_throw_exception()
        {
            ActualException.Should().BeNull();
        }
    }

    public class When_the_length_is_less_than_minimum : TestAssertArgumentLengthRangeBase
    {
        protected override void EstablishContext()
        {
            MaximumLength = 10;
            MinimumLength = 5;
            Argument = "hi";
            base.EstablishContext();
        }

        [Observation]
        void should_throw_exception()
        {
            ActualException.Should().NotBeNull();
            ActualException.Should().BeOfType<ArgumentException>();
        }

        [Observation]
        void should_has_exception_message()
        {
            ActualException.Message.Should().Be(ExpectedMessage);
        }
    }

    public class When_the_length_is_greater_than_minimum : TestAssertArgumentLengthRangeBase
    {
        protected override void EstablishContext()
        {
            MaximumLength = 10;
            MinimumLength = 5;
            Argument = "Hello world";
            base.EstablishContext();
        }

        [Observation]
        void should_throw_exception()
        {
            ActualException.Should().NotBeNull();
            ActualException.Should().BeOfType<ArgumentException>();
        }

        [Observation]
        void should_has_exception_message()
        {
            ActualException.Message.Should().Be(ExpectedMessage);
        }
    }

    public class TestAssertArgumentLengthBase: Specification
    {
        protected string Argument;

        protected Exception ActualException;

        protected string ExpectedMessage;

        protected int MaximumLength;

        protected override void EstablishContext()
        {
            ExpectedMessage = $"Should be less than {MaximumLength}";
        }

        protected override void Because()
        {
            ActualException = Record.Exception(() =>
            {
                AssertionConcern.AssertArgumentLength(Argument, MaximumLength, ExpectedMessage);
            });
        }
    }

    public class When_length_is_more_than_maximum_length : TestAssertArgumentLengthBase
    {
        protected override void EstablishContext()
        {
            MaximumLength = 4;
            Argument = "hello";
            base.EstablishContext();
        }

        [Observation]
        void should_throw_exception()
        {
            ActualException.Should().NotBeNull();
            ActualException.Should().BeOfType<ArgumentException>();
        }

        [Observation]
        void should_has_exception_message()
        {
            ActualException.Message.Should().Be(ExpectedMessage);
        }
    }

    public class When_length_is_less_than_maximum_length: TestAssertArgumentLengthBase
    {
        protected override void EstablishContext()
        {
            MaximumLength = 10;
            Argument = "hello";
            base.EstablishContext();
        }

        [Observation]
        void should_not_throw_exception()
        {
            ActualException.Should().BeNull();
        }
    }

    public class TestAssertArgumentFalseBase: Specification
    {
        protected bool Argument;

        protected Exception ActualException;

        protected string ExpectedMessage;

        protected override void EstablishContext()
        {
            ExpectedMessage = "Should be false";
        }

        protected override void Because()
        {
            ActualException = Record.Exception(() =>
            {
                AssertionConcern.AssertArgumentFalse(Argument, ExpectedMessage);
            });
        }
    }

    public class When_argument_is_false : TestAssertArgumentFalseBase
    {
        protected override void EstablishContext()
        {
            base.EstablishContext();
            Argument = false;
        }

        [Observation]
        void should_not_throw_exception()
        {
            ActualException.Should().BeNull(); 
        }
    }

    public class When_argument_is_true : TestAssertArgumentFalseBase
    {
        protected override void EstablishContext()
        {
            base.EstablishContext();
            Argument = true;
        }

        [Observation]
        void should_throw_exception()
        {
            ActualException.Should().NotBeNull();
            ActualException.Should().BeOfType<ArgumentException>();
        }

        [Observation]
        void should_has_exception_message()
        {
            ActualException.Message.Should().Be(ExpectedMessage);
        }
    }

    public class TestAssertArgumentEqualsBase : Specification
    {
        protected object Argument1; 

        protected object Argument2;

        protected Exception ActualException;

        protected string ExpectedMessage;

        protected override void EstablishContext()
        {
            ExpectedMessage = "Should be equal";
        }

        protected override void Because()
        {
            ActualException = Record.Exception(() =>
            {
                AssertionConcern.AssertArgumentEquals(Argument1, Argument2, ExpectedMessage);
            });
        }
    }

    public class When_arguments_are_equal: TestAssertArgumentEqualsBase
    {
        protected override void EstablishContext()
        {
            base.EstablishContext();
            Argument1 = 1;
            Argument2 = 1;
        }

        [Observation]
        void should_not_throw_exception()
        {
            ActualException.Should().BeNull();
        }
    }

    public class When_arguments_are_not_equal : TestAssertArgumentEqualsBase
    {
        protected override void EstablishContext()
        {
            base.EstablishContext();
            Argument1 = 1;
            Argument2 = 2;
        }

        [Observation]
        void should_throw_exception()
        {
            ActualException.Should().NotBeNull();
            ActualException.Should().BeOfType<ArgumentException>();
        }

        [Observation]
        void should_has_exception_message()
        {
            ActualException.Message.Should().Be(ExpectedMessage);
        }
    }
}
