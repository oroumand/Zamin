
using Zamin.Core.Domain.Exceptions;

namespace Zamin.Core.Domain.Toolkits.Tests.Unit.ValueObjects;
public class NationalCodeTests
{
	[Fact]
	public void ShouldBe_CreateNationalCode_When_ValidInput()
	{
		//Arrange
		string nationalCodeInput = "0123456789";

		//Act
		NationalCode nationalCodeObject = new NationalCode(nationalCodeInput);

		//Assert
		Assert.NotNull(nationalCodeObject);
		Assert.Equal(nationalCodeInput, nationalCodeObject);
	}

	[Theory]
	[InlineData("")]
	[InlineData(null)]
	[InlineData("ThisIsAWord")]
	[InlineData("0123456")]
	[InlineData("01234567890")]
	[InlineData("0123 4567	89")]
	public void ShouldBe_ThrowInvalidObjectStateException_When_InvalidInput(string input)
	{
		//Arrange
		string nationalCodeInput = input;

		//Act
		void CreateNationalCode()
		{
			NationalCode nationalCodeObject = new NationalCode(nationalCodeInput);
		}

		//Assert
		Assert.Throws<InvalidValueObjectStateException>(CreateNationalCode);
	}


	[Fact]
	public void ShouldBe_StringCastToNationalCodeObject_When_UseImplicitCast()
	{
		//Arrange
		string nationalCodeInput = "0123456789";

		//Act
		NationalCode nationalCode = nationalCodeInput;

		//Assert
		Assert.NotSame( nationalCodeInput, nationalCode);
		Assert.Same(nationalCodeInput, nationalCode.Value);

		Assert.Equal(nationalCodeInput, nationalCode);
		Assert.Equal(nationalCodeInput, nationalCode.Value);
		Assert.Equal(nationalCodeInput, nationalCode.Value.ToString());
	}

	[Fact]
	public void ShouldBe_NationalCodeObjectCastToString_When_UseExplicitCast()
	{
		//Arrange
		string nationalCode = String.Empty;
		NationalCode nationalCodeObject = new("0123456789");

		//Act
		nationalCode = (string)nationalCodeObject;

		//Assert
		Assert.NotSame(nationalCode, nationalCodeObject);
		Assert.Same(nationalCode, nationalCodeObject.Value);

		Assert.Equal(nationalCode, nationalCodeObject);
		Assert.Equal(nationalCode, nationalCodeObject.Value);
		Assert.Equal(nationalCode, nationalCodeObject.Value.ToString());
	}

	[Fact]
	public void ShouldBe_ReturnNationalCodeObject_When_CallFromStringStaticMethod()
	{
		//Arrange
		string nationalCodeInput = "0123456789";

		//Act
		var fromString = NationalCode.FromString(nationalCodeInput);

		//Assert
		Assert.IsType<NationalCode>(fromString);
		Assert.Equal(nationalCodeInput, fromString.Value);
	}

	[Fact]
	public void ShouldBe_ReturnValue_When_CallToStringMethod()
	{
		//Arrange
		string stringNationalCode = "0123456789";
		NationalCode nationalCode = new(stringNationalCode);

		//Act
		string result = nationalCode.ToString();

		//Assert
		Assert.Equal(result, stringNationalCode);
	}
}
