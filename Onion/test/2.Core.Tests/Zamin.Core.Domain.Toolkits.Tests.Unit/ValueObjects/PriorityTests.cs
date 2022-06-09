using Zamin.Core.Domain.Exceptions;

namespace Zamin.Core.Domain.Toolkits.Tests.Unit.ValueObjects;
public class PriorityTests
{
	[Fact]
	public void ShouldBe_CreatePriorityObject_When_ValidInput()
	{
		//Arrange
		int input = 1;

		//Act
		Priority priority = new(input);

		//Assert
		Assert.Equal(input, priority);

	}

	[Theory]
	[InlineData(0)]
	[InlineData(-1)]
	public void ShouldBe_ThrowInvalidValueObjectStateException_When_InputNumberLessThanOne(int input)
	{
		//Arrange
		int inputNumber = input;

		//Act
		void CreatePriority()
		{
			Priority priority = new(inputNumber);
		}

		//Assert
		Assert.Throws<InvalidValueObjectStateException>(CreatePriority);
	}

	[Fact]
	public void ShouldBe_IncreasePriorityNumber_When_CallIncreaseMethod()
	{
		//Arrange
		Priority priority = new(1);

		//Act
		var newPriority = priority.Increase(2);

		//Assert
		Assert.Equal(3, newPriority);
	}

	[Fact]
	public void ShouldBe_IncreasePriorityNumber_When_UsePlusOperator()
	{
		//Arrange
		Priority priority = new(1);

		//Act
		Priority newPriority = priority + 3;

		//Arrange
		Assert.Equal(4, newPriority);
	}

	[Fact]
	public void ShouldBe_DecreasePriorityNumber_When_CallDecreaseMethod()
	{
		//Arrange
		Priority priority = new(3);

		//Act
		var newPriority = priority.Decrease(2);

		//Assert
		Assert.Equal(1, newPriority);
	}

	[Fact]
	public void ShouldBe_DecreasePriorityNumber_When_UseMinusOperator()
	{
		//Arrange
		Priority priority = new(3);

		//Act
		var newPriority = priority - 2;

		//Assert
		Assert.Equal(1, newPriority);
	}

	[Fact]
	public void ShouldBe_InvalidValueObjectStateException_When_CallDecreaseMethodAndResultIsLessThanOne()
	{
		//Arrange
		Priority priority = new(1);

		//Act
		void DecreasePriorityLessThan1()
		{
			var newPriority = priority.Decrease(1);
			//newPriority is 0
		}

		//Assert
		Assert.Throws<InvalidValueObjectStateException>(DecreasePriorityLessThan1);
	}

	[Fact]
	public void ShouldBe_CheckPriorityLessThan_When_UseLessThanOperator()
	{
		//Arrange
		Priority priority1 = new(1);
		Priority priority2 = new(2);

		//Act
		var lessThan = priority1 < priority2;

		//Assert
		Assert.True(lessThan);
	}

	[Fact]
	public void ShouldBe_CheckPriorityLessThanOrEqual_When_UseLessThanOrEqualOperator()
	{
		//Arrange
		Priority priority1 = new(1);
		Priority priority2 = new(1);

		//Act
		var lessThan = priority1 <= priority2;

		//Assert
		Assert.True(lessThan);
	}

	[Fact]
	public void ShouldBe_CheckPriorityGreaterThan_When_UseGreaterThanOperator()
	{
		//Arrange
		Priority priority1 = new(2);
		Priority priority2 = new(1);

		//Act
		var lessThan = priority1 > priority2;

		//Assert
		Assert.True(lessThan);
	}
	[Fact]
	public void ShouldBe_CheckPriorityGreaterThanOrEqual_When_UseGreaterThanOrEqualOperator()
	{
		//Arrange
		Priority priority1 = new(2);
		Priority priority2 = new(1);

		//Act
		var lessThan = priority1 >= priority2;

		//Assert
		Assert.True(lessThan);
	}

	[Fact]
	public void ShouldBe_PriorityObjectCastToInt_When_ExplicitCast()
	{
		//Arrange
		int number = 0;
		Priority priority = new Priority(1);

		//Act
		number = (int)priority;

		//Assert
		Assert.Equal(number, priority.Value);
		Assert.Equal(1,number);
	}

	[Fact]
	public void ShouldBe_IntCastToPriorityObject_When_ImplicitCast()
	{
		//Arrange
		int number = 5;

		//Act
		Priority priority = number;

		//Assert
		Assert.Equal(number,priority.Value);
		Assert.Equal(5,priority.Value);
		Assert.Equal(5,priority);
	}

	[Fact]
	public void ShouldBe_ReturnPriorityObject_When_CallFromIntStaticMethod()
	{
		//Arrange
		int number = 3;

		//Act
		Priority fromInt = Priority.FromInt(number);
		
		//Assert
		Assert.Equal(fromInt, number);
		Assert.Equal(3,fromInt);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(-1)]
	public void ShouldBe_ThrowInvalidValueObjectStateException_When_CallFromIntStaticMethodAndNumberIsLessThan1(int inputNumber)
	{
		//Arrange
		int number = inputNumber;

		//Act
		void ConvertIntToPriority()
		{
			Priority fromInt = Priority.FromInt(number);
		}

		//Assert
		Assert.Throws<InvalidValueObjectStateException>(ConvertIntToPriority);
	}

}
