using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    const string FILE_PATH = "recipes.txt";

    static void Main(string[] args)
    {
        List<Recipe> recipes = LoadFromFile();

        bool running = true;
        while (running)
        {
            ShowMenu();
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": AddRecipe(recipes); break;
                case "2": ListRecipes(recipes); break;
                case "3": ShowRecipeDetails(recipes); break;
                case "4":
                    SaveToFile(recipes);
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
        Console.WriteLine("Goodbye!");
    }

    static void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("===============================");
        Console.WriteLine("        Recipe Manager");
        Console.WriteLine("===============================");
        Console.WriteLine("1. Add a new recipe");
        Console.WriteLine("2. List all recipes");
        Console.WriteLine("3. Show recipe details");
        Console.WriteLine("4. Exit");
        Console.WriteLine("-------------------------------");
        Console.Write("Choose an option: ");
    }

    static void AddRecipe(List<Recipe> recipes)
    {
        Console.Write("Recipe name: ");
        string name = Console.ReadLine();

        Console.Write("Description: ");
        string desc = Console.ReadLine();

        Recipe r = new Recipe(name, desc);

        Console.WriteLine("Add ingredients. Type 'done' to finish.");
        while (true)
        {
            Console.Write("  Ingredient name: ");
            string ing = Console.ReadLine();
            if (ing.Trim().ToLower() == "done") break;

            Console.Write("  Quantity in grams: ");
            string qtyStr = Console.ReadLine();

            if (!double.TryParse(qtyStr, out double qty))
            {
                Console.WriteLine("  Not a valid number. Skipping.");
                continue;
            }

            r.AddIngredient(ing, qty);
        }

        recipes.Add(r);
        Console.WriteLine($"Recipe '{name}' added.");
    }

    static void ListRecipes(List<Recipe> recipes)
    {
        if (recipes.Count == 0)
        {
            Console.WriteLine("No recipes yet.");
            return;
        }

        var sorted = new List<Recipe>(recipes);
        sorted.Sort((a, b) => string.Compare(a.Name, b.Name,
                                StringComparison.OrdinalIgnoreCase));

        Console.WriteLine("--- All recipes ---");
        for (int i = 0; i < sorted.Count; i++)
            Console.WriteLine($"{i + 1}. {sorted[i].Name}");
    }

    static void ShowRecipeDetails(List<Recipe> recipes)
    {
        Console.Write("Enter recipe name: ");
        string name = Console.ReadLine();

        Recipe r = recipes.Find(x =>
            string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));

        if (r == null)
        {
            Console.WriteLine("Recipe not found.");
            return;
        }

        r.DisplayDetails(false);

        Console.WriteLine("Options:");
        Console.WriteLine("  o = view in ounces");
        Console.WriteLine("  e = edit an ingredient quantity");
        Console.WriteLine("  r = remove an ingredient");
        Console.WriteLine("  any other key = back to menu");
        Console.Write("> ");
        string opt = (Console.ReadLine() ?? "").Trim().ToLower();

        switch (opt)
        {
            case "o":
                r.DisplayDetails(true);
                break;
            case "e":
                Console.Write("Ingredient to edit: ");
                string en = Console.ReadLine();
                Console.Write("New quantity (grams): ");
                if (double.TryParse(Console.ReadLine(), out double nq))
                    r.EditIngredient(en, nq);
                else
                    Console.WriteLine("Not a valid number.");
                break;
            case "r":
                Console.Write("Ingredient to remove: ");
                r.RemoveIngredient(Console.ReadLine());
                break;
        }
    }

    static void SaveToFile(List<Recipe> recipes)
    {
        using (StreamWriter sw = new StreamWriter(FILE_PATH))
        {
            foreach (Recipe r in recipes)
            {
                sw.WriteLine(r.Name);
                sw.WriteLine(r.Description);

                var ings = r.GetIngredientsCopy();
                var parts = new List<string>();
                foreach (var kv in ings)
                    parts.Add($"{kv.Key}={kv.Value}");

                sw.WriteLine(string.Join(";", parts));
            }
        }
        Console.WriteLine($"Saved {recipes.Count} recipe(s).");
    }

    static List<Recipe> LoadFromFile()
    {
        var list = new List<Recipe>();
        if (!File.Exists(FILE_PATH))
            return list;

        string[] lines = File.ReadAllLines(FILE_PATH);
        for (int i = 0; i + 2 < lines.Length; i += 3)
        {
            Recipe r = new Recipe(lines[i], lines[i + 1]);

            if (!string.IsNullOrWhiteSpace(lines[i + 2]))
            {
                foreach (string p in lines[i + 2].Split(';'))
                {
                    string[] kv = p.Split('=');
                    if (kv.Length == 2 && double.TryParse(kv[1], out double q))
                        r.AddIngredient(kv[0], q);
                }
            }

            list.Add(r);
        }

        Console.WriteLine($"Loaded {list.Count} recipe(s).");
        return list;
    }
}