using System;
using System.Collections.Generic;

class Recipe
{
    public string Name { get; set; }
    public string Description { get; set; }
    private Dictionary<string, double> ingredients;

    public Recipe(string name, string description)
    {
        Name = name;
        Description = description;
        ingredients = new Dictionary<string, double>();
    }

    public void AddIngredient(string name, double quantity)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Error: ingredient name cannot be empty.");
            return;
        }
        if (quantity <= 0)
        {
            Console.WriteLine("Error: quantity must be positive.");
            return;
        }
        ingredients[name.ToLower()] = quantity;
    }

    public void RemoveIngredient(string name)
    {
        string key = name.ToLower();
        if (ingredients.ContainsKey(key))
        {
            ingredients.Remove(key);
            Console.WriteLine($"Removed '{name}'.");
        }
        else
        {
            Console.WriteLine($"Ingredient '{name}' not found.");
        }
    }

    public void EditIngredient(string name, double newQuantity)
    {
        string key = name.ToLower();
        if (!ingredients.ContainsKey(key))
        {
            Console.WriteLine($"Ingredient '{name}' not found.");
            return;
        }
        if (newQuantity <= 0)
        {
            Console.WriteLine("Error: quantity must be positive.");
            return;
        }
        ingredients[key] = newQuantity;
    }

    public double ConvertToOunces(double grams)
    {
        return grams * 0.03527396;
    }

    public void DisplayIngredients(bool convertToOunces = false)
    {
        if (ingredients.Count == 0)
        {
            Console.WriteLine("(no ingredients yet)");
            return;
        }

        var sorted = new List<string>(ingredients.Keys);
        sorted.Sort();

        foreach (string key in sorted)
        {
            double qty = ingredients[key];
            if (convertToOunces)
                Console.WriteLine($"  - {key}: {ConvertToOunces(qty):F2} oz");
            else
                Console.WriteLine($"  - {key}: {qty:F2} g");
        }
    }

    public void DisplayDetails(bool showInOunces = false)
    {
        Console.WriteLine("================================");
        Console.WriteLine($"Recipe: {Name}");
        Console.WriteLine($"Description: {Description}");
        Console.WriteLine("Ingredients:");
        DisplayIngredients(showInOunces);
        Console.WriteLine("================================");
    }

    public Dictionary<string, double> GetIngredientsCopy()
    {
        return new Dictionary<string, double>(ingredients);
    }

    public override string ToString()
    {
        return $"{Name} ({ingredients.Count} ingredients)";
    }
}