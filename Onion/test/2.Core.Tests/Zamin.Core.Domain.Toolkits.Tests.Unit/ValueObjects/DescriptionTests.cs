using Zamin.Core.Domain.Exceptions;


namespace Zamin.Core.Domain.Toolkits.Tests.Unit.ValueObjects;

public class DescriptionTests
{
	[Fact]
	public void ShouldBe_ReturnNullValue_When_InputIsNull()
	{
		//Arrange
		string? input = null;

		//Act
		Description description = new Description(input);

		//Assert
		Assert.Null(description.Value);

	}

	[Fact]
	public void ShouldBe_ReturnStringValue_When_InputValueLessThan500AndNotNull()
	{
		//Arrange
		string input = "This is a short description";

		//Act
		Description description = new Description(input);

		//Assert
		Assert.IsAssignableFrom<string>(description.Value);
	}

	[Fact]
	public void ShouldBe_ThrowInvalidValueObjectStateException_When_DescriptionAreMoreThan500Characters()
	{
		//Arrange
		string char501 =
			"asdjfhkjdsfhlksdjfhskdjfhksdfjhjksdhfjsdfhsldfhskldfjsldfhksdjfhkjdsfhkjsdfhkdsfhksdjhfksdhfksdhfdhjasdjfhkjdsfhlksdjfhskdjfhksdfjhjksdhfjsdfhsldfhskldfjsldfhksdjfhkjdsfhkjsdfhkdsfhksdjhfksdhfksdhfdhjasdjfhkjdsfhlksdjfhskdjfhksdfjhjksdhfjsdfhsldfhskldfjsldfhksdjfhkjdsfhkjsdfhkdsfhksdjhfksdhfksdhfdhjasdjfhkjdsfhlksdjfhskdjfhksdfjhjksdhfjsdfhsldfhskldfjsldfhksdjfhkjdsfhkjsdfhkdsfhksdjhfksdhfksdhfdhjasdjfhkjdsfhlksdjfhskdjfhksdfjhjksdhfjsdfhsldfhskldfjsldfhksdjfhkjdsfhkjsdfhkdsfhksdjhfksdhfksdhfdhj1";

		//Act
		void CreateDescriptionWith501Characters()
		{
			Description description = new(char501);
		}

		//Assert
		Assert.Throws<InvalidValueObjectStateException>(CreateDescriptionWith501Characters);
	}

	[Theory]
	[InlineData("")]
	[InlineData(null)]
	[InlineData("Hello World")]
	public void ShouldBe_ReturnDescriptionObject_When_CallFromStringStaticMethod(string input)
	{
		//Arrange
		string stringDescription = input;

		//Act
		var fromString = Description.FromString(stringDescription);

		//Assert
		Assert.IsType<Description>(fromString);
		Assert.Equal(stringDescription, fromString.Value);
	}

	[Theory]
	[InlineData("", "")]
	[InlineData(null, null)]
	[InlineData("same", "same")]
	public void ShouldBe_TwoDescriptionObjectsAreEqual_When_TheyHaveSameValue(string input1, string input2)
	{
		//Arrange
		Description description1 = new(input1);
		Description description2 = new(input2);

		//Act
		var result1 = description1 == description2;
		//var result2 = description1.Value==description2.Value; //Test Failed for null input
		var result3 = description1.Equals(description2);
		//var result4 = description1.Value.Equals(description2.Value); //Test Failed for null input

		//Arrange
		Assert.True(result1);
		//Assert.True(result2);
		Assert.True(result3);
		//Assert.True(result4);
	}

	[Fact]
	public void ShouldBe_StringCastToDescriptionObject_When_UseImplicitCast()
	{
		//Arrange
		string hello = "hello";


		//Act
		Description descriptionObject = hello;

		//Assert
		Assert.IsType<Description>(descriptionObject);
		Assert.Equal(hello, descriptionObject);
		Assert.Equal(hello, descriptionObject.Value);
		Assert.Equal(hello, descriptionObject.Value.ToString());

	}
	[Fact]
	public void ShouldBe_StringCastToDescriptionObject_When_UseExplicitCast()
	{
		//Arrange
		string hello = "hello";

		Description descriptionObject = new(hello);

		//Act
		var description = (Description)hello;

		bool equalityCheck1 = description.Equals(descriptionObject);
		bool equalityCheck2 = description == descriptionObject;
		bool equalityCheck3 = description.Value == descriptionObject.Value;

		//Assert
		Assert.IsType<Description>(description);
		Assert.Equal(hello, description.Value);
		Assert.Equal(hello, description.Value.ToString());

		Assert.True(equalityCheck1);
		Assert.True(equalityCheck2);
		Assert.True(equalityCheck3);

	}

	[Fact]
	public void ShouldBe_DescriptionObjectCastToString_When_ExplicitCast()
	{
		//Arrange
		Description description = new("hello");
		string str = String.Empty;

		//Act
		str = (string)description;

		//Assert
		Assert.Equal("hello", str);
	}

}
