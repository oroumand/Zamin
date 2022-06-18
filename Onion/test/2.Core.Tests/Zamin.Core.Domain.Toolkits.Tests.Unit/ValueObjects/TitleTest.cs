using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamin.Core.Domain.Exceptions;

namespace Zamin.Core.Domain.Toolkits.Tests.Unit.ValueObjects;
public class TitleTest
{
	[Fact]
	public void ShouldBe_CreatedTitleObject_When_PassValidInput()
	{
		//Arrange
		string stringTitle = "This is Title";

		//Act
		Title title = new(stringTitle);

		//Assert
		Assert.Equal(stringTitle,title);
		Assert.Equal(stringTitle,title.Value);
		Assert.Equal(stringTitle,title.ToString());
		Assert.Equal(stringTitle,title.Value.ToString());
	}

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData(" ")]
	[InlineData("	")]
	public void ShouldBe_ThrowInvalidObjectStateException_When_InputIsNullOrEmpty(string input)
	{
		//Arrange
		string inputString = input;

		//Act
		void CreateTitle()
		{
			Title title = new(inputString);
		}

		//Assert
		Assert.Throws<InvalidValueObjectStateException>(CreateTitle);
	}

	[Theory]
	[InlineData("1")]
	[InlineData("asdjfhkjdsfhlksdjfhskdjfhksdfjhjksdhfjsdfhsldfhskldfjsldfhksdjfhkjdsfhkjsdfhkdsfhksdjhfksdhfksdhfdhjasdjfhkjdsfhlksdjfhskdjfhksdfjhjksdhfjsdfhsldfhskldfjsldfhksdjfhkjdsfhkjsdfhkdsfhksdjhfksdhfksdhfdhjasdjfhkjdsfhlksdjfhskdjfhksdfjhjksdhfjsdfhsldfhskl4")]
	public void ShouldBe_ThrowInvalidObjectStateException_When_InputIsLessThan2OrMoreThan250Characters(string input)
	{
		//Arrange
		string inputString = input;

		//Act
		void CreateTitle()
		{
			Title title = new(inputString);
		}

		//Assert
		Assert.Throws<InvalidValueObjectStateException>(CreateTitle);
	}

	[Fact]
	public void ShouldBe_StringToTitleObjectCast_When_UseExplicitCast()
	{
		//Arrange
		string name = string.Empty;
		Title title=new("hello");

		//Act
		name =(string) title;

		//Assert
		Assert.Equal("hello",name);
	}

	[Fact]
	public void ShouldBe_TitleObjectToStringCast_When_UseImplicitCast()
	{
		//Arrange
		string name = "hello";

		Title title=new("This is Title");

		//Act
		title = name;

		//Assert
		Assert.Equal("hello",title);
		Assert.Equal("hello",title.Value);
		Assert.Equal("hello",title.ToString());
	}


	[Fact]
	public void ShouldBe_ReturnTitleObject_When_CallFromStringStaticMethod()
	{
		//Arrange
		string name = "Hello World.";

		//Act
		Title title=Title.FromString(name);

		//Assert
		Assert.Equal(name,title);
		Assert.Equal(name,title.Value);
		Assert.Equal(name,title.ToString());
	}

}
