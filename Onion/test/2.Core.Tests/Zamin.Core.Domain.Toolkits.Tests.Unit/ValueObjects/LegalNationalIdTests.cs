
using Zamin.Core.Domain.Exceptions;

namespace Zamin.Core.Domain.Toolkits.Tests.Unit.ValueObjects;
public class LegalNationalIdTests
{
	[Fact]
	public void ShouldBe_CreatedLegalNationalId_When_ValidInput()
	{
		//Arrange
		string nationalCodeId = "10320845857";

		//Act
		LegalNationalId nationalId = new(nationalCodeId);

		//Assert
		Assert.IsType<LegalNationalId>(nationalId);
		Assert.Equal(nationalCodeId, nationalId.Value);
	}



	[Theory]
	[InlineData("hello")]
	[InlineData("he46546")]
	[InlineData("1111111111")]
	[InlineData("11111 1111")]
	[InlineData("Hello World and Salam Donya")]
	[InlineData("")]
	[InlineData(null)]
	[InlineData("	")]
	public void ShouldBe_ThrowInvalidValueObjectStateException_When_InvalidInput(string input)
	{
		//Arrange
		string userInput = String.Empty;
		userInput = input;
		//Act
		void CreateNationalId()
		{
			LegalNationalId nationalId = new(userInput);
		}


		//Assert
		Assert.Throws<InvalidValueObjectStateException>(CreateNationalId);
	}


	[Theory]
	[InlineData("11111 11111")] //System.FormatException: 'Input string was not in a correct format.'
	[InlineData("11111 11112")]
	[InlineData("01234 56789")]
	[InlineData("22222 22222")]
	public void ShouldBe_ThrowSystemFormatException_When_InvalidInput(string input)
	{
		//Arrange
		string userInput = String.Empty;
		userInput = input;
		//Act
		void CreateNationalId()
		{
			LegalNationalId nationalId = new(userInput);
		}


		//Assert
		Assert.Throws<FormatException>(CreateNationalId);
	}


	[Fact]
	public void ShouldBe_StringCastToLegalNationalIdObject_When_UseImplicitCast()
	{
		//Arrange
		string legalNationalCode = "10320845857";

		//Act
		LegalNationalId legalNationalId = legalNationalCode;

		//Assert
		Assert.NotSame(legalNationalId, legalNationalCode);
		Assert.Same(legalNationalId.Value, legalNationalCode);

		Assert.Equal(legalNationalCode, legalNationalId);
		Assert.Equal(legalNationalCode, legalNationalId.Value);
		Assert.Equal(legalNationalCode, legalNationalId.Value.ToString());
	}

	[Fact]
	public void ShouldBe_LegalNationalIdObjectCastToString_When_UseExplicitCast()
	{
		//Arrange
		string legalNationalCode = String.Empty;
		LegalNationalId legalNationalId = new("10320845857");

		//Act
		legalNationalCode = (string)legalNationalId;

		//Assert
		Assert.NotSame(legalNationalCode, legalNationalId);
		Assert.Same(legalNationalCode, legalNationalId.Value);

		Assert.Equal(legalNationalCode, legalNationalId);
		Assert.Equal(legalNationalCode, legalNationalId.Value);
		Assert.Equal(legalNationalCode, legalNationalId.Value.ToString());
	}

	[Fact]
	public void ShouldBe_ReturnLegalNationalIdObject_When_CallFromStringStaticMethod()
	{
		//Arrange
		string stringLegalNationalId= "10320845857";

		//Act
		var fromString = LegalNationalId.FromString(stringLegalNationalId);

		//Assert
		Assert.IsType<LegalNationalId>(fromString);
		Assert.Equal(stringLegalNationalId, fromString.Value);
	}

	[Fact]
	public void ShouldBe_ReturnValue_When_CallToStringMethod()
	{
		//Arrange
		string stringLegalNationalId = "10320845857";
		LegalNationalId legalNationalId = new("10320845857");

		//Act
		string result = legalNationalId.ToString();

		//Assert
		Assert.Equal(result,stringLegalNationalId);
	}
}
