using FluentValidation.TestHelper;
using PizzaCleanApp.Core.Features.Pizzas.Create;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;
using PizzaCleanApp.UnitTests.TestSetup;
using Shouldly;

namespace PizzaCleanApp.UnitTests.Pizzas;

public class CreatePizzaUseCaseTests
{
    #region Validation outlines

    [Fact]
    public async Task Pizza_fails_validation_when_name_is_empty()
    {
        var validator = new CreatePizzaValidator();
        var request = new CreatePizzaRequest
        (
            Name: "",
            Description: "Description with empty name.",
            BasePrice: 9.99m,
            ToppingIds: new long[] { 1 }
        );

        var result = await validator.TestValidateAsync(request);

        result.ShouldHaveValidationErrorFor(r => r.Name);
    }

    [Fact]
    public async Task Pizza_fails_validation_when_name_exceeds_max_length()
    {
        var validator = new CreatePizzaValidator();
        var longName = new string('A', 101);
        var request = new CreatePizzaRequest
        (
            Name: longName,
            Description: "Description with long name.",
            BasePrice: 9.99m,
            ToppingIds: new long[] { 1 }
        );
        var result = await validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(r => r.Name);
    }

    [Fact]
    public async Task Pizza_fails_validation_when_description_exceeds_max_length()
    {
        var validator = new CreatePizzaValidator();
        var longDescription = new string('B', 501);
        var request = new CreatePizzaRequest
        (
            Name: "Valid Name",
            Description: longDescription,
            BasePrice: 9.99m,
            ToppingIds: new long[] { 1 }
        );
        var result = await validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(r => r.Description);
    }

    [Fact]
    public async Task Pizza_fails_validation_when_base_price_is_negative()
    {
        var validator = new CreatePizzaValidator();
        var request = new CreatePizzaRequest
        (
            Name: "Negative Price Pizza",
            Description: "Description with negative price.",
            BasePrice: -5.00m,
            ToppingIds: new long[] { 1 }
        );
        var result = await validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(r => r.BasePrice);
    }

    [Fact]
    public async Task Pizza_fails_validation_when_topping_id_is_negative()
    {
        var validator = new CreatePizzaValidator();
        var request = new CreatePizzaRequest
        (
            Name: "Invalid Topping Pizza",
            Description: "Description with invalid topping id.",
            BasePrice: 9.99m,
            ToppingIds: new long[] { -1 }
        );
        var result = await validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(r => r.ToppingIds);
    }

    #endregion

    #region Failure outlines

    [Fact]
    public async Task Pizza_is_not_created_when_name_already_exists()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var createPizzaUseCase = CreateUseCase(context);
        var request = new CreatePizzaRequest
        (
            Name: "Pepperoni",
            Description: "Classic pizza with pepperoni slices.",
            BasePrice: 8.99m,
            ToppingIds: new long[] { 1 }
        );
        
        Result<CreatePizzaResponse> result = await createPizzaUseCase.ExecuteAsync(request);

        result.IsSuccess.ShouldBeFalse();
        result.ErrorType.ShouldBe(ErrorType.Conflict);
    }

    [Fact]
    public async Task Pizza_is_not_created_when_topping_does_not_exist()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var createPizzaUseCase = CreateUseCase(context);
        var request = new CreatePizzaRequest
        (
            Name: "Veggie Delight",
            Description: "A delightful mix of fresh vegetables.",
            BasePrice: 7.99m,
            ToppingIds: new long[] { 999 } // Assuming 999 does not exist
        );
        Result<CreatePizzaResponse> result = await createPizzaUseCase.ExecuteAsync(request);
        result.IsSuccess.ShouldBeFalse();
        result.ErrorType.ShouldBe(ErrorType.NotFound);
    }

    [Fact]
    public async Task Pizza_is_not_created_when_topping_is_inactive()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var createPizzaUseCase = CreateUseCase(context);
        var request = new CreatePizzaRequest
        (
            Name: "Hawaiian",
            Description: "Pizza with ham and pineapple.",
            BasePrice: 9.49m,
            ToppingIds: new long[] { 7 } // Assuming 5 is an inactive topping
        );
        
        Result<CreatePizzaResponse> result = await createPizzaUseCase.ExecuteAsync(request);
        result.IsSuccess.ShouldBeFalse();
        result.ErrorType.ShouldBe(ErrorType.NotFound);
    }

    #endregion

    #region Successful creation outlines
    // Successful creation outlines

    [Fact]
    public async Task Pizza_is_created_when_name_is_unique_and_no_toppings()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var createPizzaUseCase = CreateUseCase(context);
        var request = new CreatePizzaRequest
        (
            Name: "Margarita",
            Description: "Classic pizza with tomato and mozzarella.",
            BasePrice: 7.49m,
            ToppingIds: Array.Empty<long>() // No toppings
        );
        
        Result<CreatePizzaResponse> result = await createPizzaUseCase.ExecuteAsync(request);

        result.IsSuccess.ShouldBeTrue();
        var response = result.Value;
        response.Id.ShouldBeGreaterThan(0);
        response.Name.ShouldBe("Margarita");
        response.Description.ShouldBe("Classic pizza with tomato and mozzarella.");
    }

    [Fact]
    public async Task Pizza_is_created_when_name_is_unique_and_all_toppings_are_active()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var createPizzaUseCase = CreateUseCase(context);
        var request = new CreatePizzaRequest
        (
            Name: "Deluxe",
            Description: "Pizza with all the deluxe toppings.",
            BasePrice: 9.99m,
            ToppingIds: new long[] { 1, 2, 3 } // Assuming these are active toppings
        );
        
        Result<CreatePizzaResponse> result = await createPizzaUseCase.ExecuteAsync(request);

        result.IsSuccess.ShouldBeTrue();
        var response = result.Value;
        response.Id.ShouldBeGreaterThan(0);
        response.Name.ShouldBe("Deluxe");
        response.Description.ShouldBe("Pizza with all the deluxe toppings.");
    }

    [Fact]
    public async Task Response_contains_persisted_Id_Name_and_Description_on_success()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var createPizzaUseCase = CreateUseCase(context);
        var request = new CreatePizzaRequest
        (
            Name: "Mushroom",
            Description: "Pizza topped with fresh mushrooms.",
            BasePrice: 8.49m,
            ToppingIds: new long[] { 4 }
        );
        
        Result<CreatePizzaResponse> result = await createPizzaUseCase.ExecuteAsync(request);

        result.IsSuccess.ShouldBeTrue();
        var response = result.Value;
        response.Id.ShouldBeGreaterThan(0);
        response.Name.ShouldBe("Mushroom");
        response.Description.ShouldBe("Pizza topped with fresh mushrooms.");
    }

    [Fact]
    public async Task Pizza_is_created_and_BasePrice_is_persisted_on_success()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var createPizzaUseCase = CreateUseCase(context);
        var request = new CreatePizzaRequest
        (
            Name: "Cheese Burst",
            Description: "Pizza with extra cheese.",
            BasePrice: 10.99m,
            ToppingIds: new long[] { 1 }
        );
        
        Result<CreatePizzaResponse> result = await createPizzaUseCase.ExecuteAsync(request);

        result.IsSuccess.ShouldBeTrue();
        var response = result.Value;
        response.Id.ShouldBeGreaterThan(0);
        response.BasePrice.ShouldBe(10.99m);        
    }

    [Fact]
    public async Task PizzaToppings_count_matches_unique_requested_topping_ids_on_success()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var createPizzaUseCase = CreateUseCase(context);
        var request = new CreatePizzaRequest
        (
            Name: "Meat Feast",
            Description: "Pizza with a variety of meats.",
            BasePrice: 11.99m,
            ToppingIds: new long[] { 1, 2, 3, 4 }
        );
        
        Result<CreatePizzaResponse> result = await createPizzaUseCase.ExecuteAsync(request);

        result.IsSuccess.ShouldBeTrue();
        var response = result.Value;
        response.Id.ShouldBeGreaterThan(0);
        response.ToppingIds.Count.ShouldBe(4); // Assuming all toppings are new and added
    }

    [Fact]
    public async Task Null_topping_ids_are_treated_as_empty_and_creation_succeeds()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var createPizzaUseCase = CreateUseCase(context);
        var request = new CreatePizzaRequest
        (
            Name: "Seafood Delight",
            Description: "Pizza with a delightful mix of seafood.",
            BasePrice: 12.99m,
            ToppingIds: null // Null ToppingIds
        );
        
        Result<CreatePizzaResponse> result = await createPizzaUseCase.ExecuteAsync(request);

        result.IsSuccess.ShouldBeTrue();
        var response = result.Value;
        response.Id.ShouldBeGreaterThan(0);
        response.ToppingIds.ShouldNotBeNull();
        response.ToppingIds.Count.ShouldBe(0);
    }

    [Fact]
    public async Task Duplicate_topping_ids_are_deduplicated_and_creation_succeeds()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var createPizzaUseCase = CreateUseCase(context);
        var request = new CreatePizzaRequest
        (
            Name: "Veggie Supreme",
            Description: "Pizza with a supreme mix of vegetables.",
            BasePrice: 9.49m,
            ToppingIds: new long[] { 1, 1, 2, 3, 3 } // Duplicate ToppingIds
        );
        
        Result<CreatePizzaResponse> result = await createPizzaUseCase.ExecuteAsync(request);

        result.IsSuccess.ShouldBeTrue();
        var response = result.Value;
        response.Id.ShouldBeGreaterThan(0);
        response.ToppingIds.Count.ShouldBe(3); // Assuming toppings 1, 2, and 3 are unique and added
    }

    #endregion

    private static CreatePizzaUseCase CreateUseCase(IDbContext dbContext)
        => new CreatePizzaUseCase(dbContext);
}
