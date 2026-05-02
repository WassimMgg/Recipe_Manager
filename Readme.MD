# 🍳 Recipe Manager

A simple C# console application to create and manage cooking recipes. Built as a hands-on exercise to practice **Object-Oriented Programming** concepts in C#.

---

## 📋 Features

- Add new recipes with a name, description, and ingredients
- List all saved recipes sorted alphabetically
- View full recipe details
- View ingredient quantities in **grams** or **ounces**
- Edit ingredient quantities
- Remove ingredients from a recipe
- Recipes are automatically **saved to a file** and **loaded on startup**

---

## 🗂️ Project Structure

```
Recipe_manager/
    Program.cs      — main program, menu loop, file I/O
    Recipe.cs       — Recipe class definition
    recipes.txt     — auto-generated data file (created on first exit)
```

---

## 🚀 How to Run

**Requirements:**
- [.NET SDK 8.0 or later](https://dotnet.microsoft.com/download)

**Steps:**

1. Clone the repository
```bash
git clone https://github.com/WassimMgg/Recipe_Manager.git
cd recipe-manager
```

2. Run the project
```bash
dotnet run
```

---

## 🖥️ Menu Options

```
===============================
        Recipe Manager
===============================
1. Add a new recipe
2. List all recipes
3. Show recipe details
4. Exit
-------------------------------
Choose an option:
```

| Option | Description |
|--------|-------------|
| 1 | Add a new recipe with ingredients |
| 2 | List all recipes alphabetically |
| 3 | View details, edit or remove ingredients |
| 4 | Save all recipes to file and exit |

---

## 💾 Data File Format

Recipes are saved in `recipes.txt`. Each recipe takes exactly **3 lines**:

```
Recipe Name
Recipe Description
ingredient1=quantity;ingredient2=quantity;ingredient3=quantity
```

Example:
```
Pancakes
Fluffy American breakfast pancakes
flour=200;milk=300;egg=100;sugar=30;butter=20
```

> ⚠️ Do not add blank lines between recipes or the file will not load correctly.

---

## 📐 OOP Concepts Used

| Concept | Where |
|--------|-------|
| Classes | `Recipe` class in `Recipe.cs` |
| Encapsulation | Private `ingredients` dictionary with public methods |
| Properties | `Name` and `Description` with `get/set` |
| Constructor | Initializes fields and the dictionary |
| Method overriding | `ToString()` overridden for readable output |
| Default parameters | `DisplayIngredients(bool convertToOunces = false)` |

---

## 📖 What I Learned

- How to design and build a class from a written specification
- The difference between fields, properties, constructors, and methods
- How to use `Dictionary<string, double>` to store key-value data
- How to validate user input with `double.TryParse`
- How to read and write data to a text file with `StreamReader` / `StreamWriter`
- How to use `List<T>` to manage a collection of objects

---

## 🛠️ Built With

- C# / .NET 8
- Visual Studio Code
