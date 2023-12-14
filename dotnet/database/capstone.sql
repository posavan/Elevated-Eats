﻿USE master
GO

--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

--create tables
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL
	CONSTRAINT PK_user PRIMARY KEY (user_id)
)

CREATE TABLE ingredients (
    ingredient_id int IDENTITY (1,1) NOT NULL,
	ingredient_name varchar(60) UNIQUE NOT NULL,
	CONSTRAINT PK_ingredient PRIMARY KEY (ingredient_id)
)
CREATE TABLE recipes (
    recipe_id int IDENTITY (1,1) NOT NULL,
	user_id int,
	recipe_name varchar(60) NOT NULL,
	recipe_instructions varchar(5000) NOT NULL,
	recipe_image varchar(500),
	CONSTRAINT PK_recipe PRIMARY KEY (recipe_id)
)

--CREATE TABLE users_saved_recipes (
--	user_recipe_id int IDENTITY (1,1) NOT NULL,
--	user_id int NOT NULL,
--	recipe_name varchar(60) NOT NULL,
--	recipe_instructions varchar(500) NOT NULL,
--	CONSTRAINT PK_user_saved_recipe PRIMARY KEY (user_recipe_id),
--	CONSTRAINT FK_user_saved_recipe_users FOREIGN KEY (user_id) REFERENCES users (user_id)
--)

CREATE TABLE recipes_ingredients (
    --user_recipe_id int NOT NULL,
	recipe_id int NOT NULL,
	ingredient_id int NOT NULL,
	quantity varchar(60) NOT NULL,
	CONSTRAINT PK_recipes_ingredients PRIMARY KEY (recipe_id, ingredient_id),
	CONSTRAINT FK_recipes_ingredients_recipes FOREIGN KEY (recipe_id)
	REFERENCES recipes (recipe_id),
    CONSTRAINT FK_recipes_ingredients_ingredients FOREIGN KEY (ingredient_id) 
	REFERENCES ingredients (ingredient_id)
)

CREATE TABLE meals (
    meal_id int IDENTITY (1,1) NOT NULL,
    meal_name varchar(60) NOT NULL,
    meal_description varchar(500) NOT NULL,
	meal_image varchar(500),
    CONSTRAINT PK_meals PRIMARY KEY (meal_id),
)

CREATE TABLE meals_recipes (
	meal_id int NOT NULL,
	recipe_id int NOT NULL,
	recipe_name varchar(60),
	CONSTRAINT PK_meals_recipes PRIMARY KEY (meal_id, recipe_id),
	CONSTRAINT FK_meals_recipes_meals FOREIGN KEY (meal_id) 
	REFERENCES meals (meal_id),
	CONSTRAINT FK_meals_recipes_recipes FOREIGN KEY (recipe_id) 
	REFERENCES recipes (recipe_id)
)

CREATE TABLE meal_plans (
    meal_plan_id int IDENTITY (1,1) NOT NULL,
	meal_plan_name varchar(60),
	meal_plan_description varchar(60),
    user_id int NOT NULL,
	meal_plan_image varchar(500),
    CONSTRAINT PK_meal_plans PRIMARY KEY (meal_plan_id),
    CONSTRAINT FK_meal_plans_users FOREIGN KEY (user_id) 
	REFERENCES users (user_id)
)

CREATE TABLE meal_plans_meals (
	meal_plan_id int NOT NULL,
    meal_id int NOT NULL,
    meal_name varchar(60),
	CONSTRAINT PK_meal_plans_meals PRIMARY KEY (meal_plan_id, meal_id),
	CONSTRAINT FK_meal_plans_meal_plans FOREIGN KEY (meal_plan_id) REFERENCES meal_plans (meal_plan_id),
	CONSTRAINT FK_meal_plans_meals FOREIGN KEY (meal_id) REFERENCES meals (meal_id)
)

--populate default data
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

INSERT INTO ingredients (ingredient_name) VALUES ( 'Rice' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Eggs' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Milk' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'basil' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'cumin' )

--INSERT INTO recipes (recipe_name, recipe_instructions ) VALUES ( 'Calzone' , 'Delicious Italian Delicacy')
--INSERT INTO recipes (recipe_name, recipe_instructions ) VALUES ( 'Carbonara' , 'Carbs')
--INSERT INTO recipes (recipe_name, recipe_instructions ) VALUES ( 'Tapioca' , 'yummy yummy')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions ) VALUES ( 1, 'Homemade Lasagna' , '1. Preheat the oven to 350°F. In a large pot of salted water, boil lasagna noodles until al dente according to package directions. 
Drain, rinse under cold water, and set aside. 2. In a large skillet or dutch oven, brown beef, sausage, onion, and garlic over medium-high heat until no pink remains. Drain any fat.
3. Stir in the pasta sauce, tomato paste, Italian seasoning, ½ teaspoon of salt, and ¼ teaspoon of black pepper. Simmer uncovered over medium heat for 5 minutes or until thickened.
4. In a separate bowl, combine 1 ½ cups mozzarella, ¼ cup parmesan cheese, ricotta, parsley, egg, and ¼ teaspoon salt.
5. Spread 1 cup of the meat sauce in a 9x13 pan or casserole dish. Top it with 3 lasagna noodles. Layer with ⅓ of the ricotta cheese mixture and 1 cup of meat sauce. Repeat twice more. Finish with 3 noodles topped with remaining sauce.
6. Cover with foil and bake for 45 minutes. 
7. Remove the foil and sprinkle with the remaining 2 ½ cups mozzarella cheese and ¼ cup parmesan cheese. Bake for an additional 15 minutes or until browned and bubbly. Broil for 2-3 minutes if desired.
8. Rest for at least 15 minutes before cutting.')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions ) VALUES ( 1, 'Mexican Corn Dip' , '1. In a slow cooker, mix together the corn, rotel, cream cheese, shredded cheese, green onions, garlic, and chili powder. 2. Cook on high for an hour or on low for a few hours.')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions ) VALUES ( 1, 'Homemade Mozzarella Sticks' , '1. Line a rimmed baking sheet with parchment paper, set aside. 
2. In a small shallow bowl, mix the flour with salt, pepper, and garlic powder.
3. In a second bowl, add eggs along with 1 tbsp of water and beat until smooth.
4. In a third bowl, mix together the panko and the italian seasoning.
5. Working with one piece of cheese at a time, dip the cheese into the egg mixture, followed by the flour mixture, back into the egg mixture and finally into the panko, gently pressing the panko to ensure it sticks. Set the cheese sticks on the prepared baking sheet and repeat with remaining cheese. 
Place the baking sheet in the freezer and freeze the cheese sticks for 45 minutes.
6. In a large pot, set over medium high heat, bring the oil to 350°F, using a deep fat thermometer. Remove the cheese sticks from the freezer. Working in small batches gently drop them into the hot oil. Cook until golden brown on all sides, about 2 minutes. 
Remove from oil and place on a metal cooling rack set over a baking sheet. Repeat with remaining cheese sticks. 
7. Serve mozzarella sticks immediately with marinara sauce.')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions ) VALUES ( 1, 'Caesar Salad' , 'Dressing: In a small bowl, whisk together garlic, dijon, worcestershire, lemon juice, and red wine vinegar. Slowly drizzle in EVOO while whisking constantly. Season to taste. 
Salad: Rinse, dry, and chop the romaine into bite sized pieces. Place in a large serving bowl and sprinkle generously with shredded parmesan and croutons. Drizzle with caesar dressing and toss gently until lettuce is evenly coated.')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions ) VALUES ( 1, 'Denver Omelet', '1. Beat eggs, milk and salt & pepper with a fork. Set aside.
2. Melt butter in a 6” skillet over medium heat. Add onion and cook until slightly softened about 3 minutes.
3. Add ham and peppers. Cook an additional 3-5 minute or until peppers are tender.
4. Turn heat to medium-low and add egg mixture. Allow to cook about 2 minutes.
5. Run the spatula along the edges of the egg mixture while lifting the pan to allow raw egg to run underneath the cooked eggs.
6. Once the eggs are almost set, add cheese and cover. Cook until top is set and cheese is melted, about 5-6 minutes.
7. Fold in half and serve.')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions ) VALUES ( 1, 'Club Sandwiches' , '1. Arrange 2 slices of bread on a cutting board. Spread each slice with mayonnaise.
2.  Top with turkey, tomato slices and cheddar cheese.
3. Spread mayonnaise on 2 more pieces of bread and place on the cheddar cheese.
4. Top with ham, bacon and lettuce. Spread mayonnaise on the final 2 slices of bread and place on top.
5. Cut sandwich into halves or quarters. Serve with pickles.')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions ) VALUES (1, 'Roasted Parmesan Asparagus','1. Preheat the oven to 425°F.
2. Rinse asparagus and pat dry. Break off the woody ends.
3. Toss asparagus with olive oil, garlic, salt and pepper and place on a baking pan.
4. Roast 6-10 minutes or until tender-crisp. Add parmesan cheese and broil 1 min.')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions ) VALUES (1, 'Breakfast Burritos', '1. Heat a 12" skillet over medium-high heat and add breakfast sausage, crumbling while cooking. Once browned, spoon onto a paper towel-lined plate leaving about 2 tablespoons of fat in the pan.
2. Add frozen hash browns (and bell peppers if using) to the pan and cook for about 10 minutes or until fully cooked and lightly browned.
3. In a small bowl combine eggs, milk and salt & black to taste. Whisk until fully combined.
4. Remove hash browns from the pan and set aside. Add 1 teaspoon of oil to the pan if needed and reduce heat to medium low.
5. Add eggs and cook until just about set, they will continue cooking as they cool.
6. Arrange 8 pieces of foil with a flour tortilla in the center of each. Divide hash brown mixture, eggs, cheese and salsa over tortillas.
7. Enjoy immediately or wrap the burritos in the foil and store it in a plastic zip-top bag in the freezer.')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions ) VALUES (1, 'Grilled Chicken','1. Combine the olive oil, lemon juice, garlic, oregano, chili powder, salt and pepper in a small bowl. Whisk until well combined.
2. Place the chicken breasts in a zip-top plastic bag and pound them using a meat mallet or rolling pin until they are an even thickness, about ½-inch thick.
3. Pour the marinade into the bag with the chicken, seal the bag, and place it in a baking dish (in case of leaks). Turn the bag to coat all parts of the chicken. Place chicken in the refrigerator for 30 minutes to 2 hours.
4. Clean the grates of a gas or charcoal grill (or use an indoor grill pan). Oil the grill grates and heat the grill to medium high (375-450 degrees F).
5. Remove the chicken from the marinade and discard extra marinade. Grill chicken for 5-8 minutes per side, until cooked through. The internal temperature of the chicken should reach 165 degrees F. For the best grill marks and juicy chicken, begin cooking the chicken on a hotter part of the grill for a few minutes and then move it to lower heat to finish cooking through.
6. Let chicken rest for 5 minutes before serving.')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions) VALUES (1, 'Chicken Stir-Fry' , '1. Heat 1 tablespoon oil in a saute pan over medium heat. Add garlic and stir. Place the chicken in the pan and brown 4 minutes on each side. Remove from pan, slice into strips, set aside.
2. Heat remaining tablespoon of oil in a wok over high heat. Add the vegetables and teriyaki sauce. Stir-fry quickly until the vegetables begin to soften. Add the chicken strips, combine well and continue to cook for 2 to 3 minutes. Serve immediately.')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions) VALUES (1, 'Buffalo Chicken Enchiladas' , 'Preheat the oven to 400 degrees F. Butter a 9-by-13-inch baking dish.
2. Mix the chicken, cream cheese, 1 cup of the Cheddar, 1/3 cup of the hot sauce, white parts of the scallions and cumin in a large bowl until well combined. Stir together the butter, remaining 2/3 cup hot sauce and 3 tablespoons water in a medium bowl. 
3. Microwave the tortillas in batches until warm, softened and foldable, about 30 seconds. Keep warm between damp paper towels. 
4.Spoon a portion of the chicken mixture down the middle of each tortilla and roll up. Place them side by side, seam-side down, in the prepared pan. Pour the hot sauce mixture over the tortillas. Sprinkle with the remaining 1 cup Cheddar and the blue cheese and bake until the cheese is melted and bubbly, 15 to 17 minutes. 
5. Drizzle the blue cheese dressing over the enchiladas and sprinkle with the scallion greens. Serve with more hot sauce.' )

INSERT INTO meals (meal_name, meal_description) VALUES ('Breakfast', 'Sunrise Sustenance');
INSERT INTO meals (meal_name, meal_description) VALUES ('Lunch', 'Mid-day Munching');
INSERT INTO meals (meal_name, meal_description) VALUES ('Dinner', 'Nighttime Nourishment');
INSERT INTO meals (meal_name, meal_description) VALUES ('Appetizers', 'Bite-Sized Beginnings');
INSERT INTO meals (meal_name, meal_description) VALUES ('Snacks', 'Tasty Treats');
INSERT INTO meals (meal_name, meal_description) VALUES ('Desserts', 'Feast Finale');





insert into meals_recipes (meal_id, recipe_id) VALUES (1,1);
insert into meals_recipes (meal_id, recipe_id) VALUES (1,3);
insert into meals_recipes (meal_id, recipe_id) VALUES (1,5);
insert into meals_recipes (meal_id, recipe_id) VALUES (2,2);
insert into meals_recipes (meal_id, recipe_id) VALUES (3,1);
insert into meals_recipes (meal_id, recipe_id) VALUES (3,5);

INSERT INTO meal_plans (user_id, meal_plan_name, meal_plan_description) VALUES (1, 'Vegan', 'for those allergic to meat');  
INSERT INTO meal_plans (user_id, meal_plan_name, meal_plan_description) VALUES (1, 'Leftovers', 'for the cheap');  
INSERT INTO meal_plans (user_id, meal_plan_name, meal_plan_description) VALUES (2, 'Bulk Prep', 'the gains');
INSERT INTO meal_plans (user_id, meal_plan_name, meal_plan_description) VALUES (2, 'vegetarian', 'gross');

insert into meal_plans_meals (meal_plan_id, meal_id, meal_name) VALUES (1, 1, 'Breakfast');
insert into meal_plans_meals (meal_plan_id, meal_id, meal_name) VALUES (1, 3, 'Dinner');
insert into meal_plans_meals (meal_plan_id, meal_id, meal_name) VALUES (2, 1, 'Breakfast');

--ingredients for lasagna
INSERT INTO ingredients (ingredient_name) VALUES ( 'Lasagna noodles' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Shredded Mozzarella' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Shredded Parmesan' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Ground Beef' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Italian Sausage' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Onion' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Garlic' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Pasta Sauce' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Tomato Paste' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Italian Seasoning' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Egg' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Ricotta Cheese' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Fresh Parsley' )


INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 1, 6, '12 noodles, uncooked');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 1, 7, '4 cups, divided');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 1, 8, '1/2 cup, divided');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 1, 9, '1/2 lb.');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 1, 10, '1/2 lb.');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 1, 11, '1, diced');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 1, 12, '2 cloves, minced');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 1, 13, '36 oz.');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 1, 14, '2 tbsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 1, 15, '1 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 1, 16, '1');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 1, 17, '2 cups');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 1, 18, '1/4 cup, chopped');


--ingredients for Mexican Corn Dip
INSERT INTO ingredients (ingredient_name) VALUES ( 'Rotel Diced Tomatoes & Green Chiles, Original' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Canned Corn' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Mexican Shredded Cheese' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Green Onions' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Chili Powder' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Cream Cheese' )


INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 2, 20, '3 (11 oz.) cans, drained');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 2, 19, '10 oz. can, drained');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 2, 21, '1/2 cup');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 2, 22, '1/4 cup');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 2, 23, '1/2 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 2, 12, '1/2 tsp, minced');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 2, 24, '8 oz., softened');


--ingredients for mozzarella sticks
INSERT INTO ingredients (ingredient_name) VALUES ( 'All-Purpose Flour' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Salt' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Black Pepper' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Garlic Powder' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Panko Breadcrumbs' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'String Cheese' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Marinara' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Vegetable Oil' )

INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 3, 25, '3/4 cup');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 3, 26, '2 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 3, 27, '1/2 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 3, 28, '1 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 3, 16, '2');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 3, 29, '2 cups');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 3, 30, '12 oz. package, cheeses cut in half');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 3, 31, '1 cup');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 3, 32, '1 quart, for frying');


--ingredients for Caesar Salad 
INSERT INTO ingredients (ingredient_name) VALUES ( 'Dijon Mustard' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Worcestershire Sauce' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Lemon Juice' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Red Wine Vinegar' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Extra Virgin Olive Oil' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Romaine Lettuce' )

INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 4, 12, '2 small cloves, minced');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 4, 33, '2 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 4, 34, '1 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 4, 35, '2 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 4, 36, '1 1/2 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 4, 37, '1/3 cup');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 4, 38, '1 large, or 2 small');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 4, 8, '1/3 cup');


--ingredients for Denver Omelet
INSERT INTO ingredients (ingredient_name) VALUES ( 'Butter' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Ham' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Green Bell Pepper' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Red Bell Pepper' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Cheddar Cheese' )

INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 5, 39, '1 tbsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 5, 2, '3');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 5, 3, '1 tbsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 5, 11, '2 tbsp, diced');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 5, 40, '1 oz, diced');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 5, 41, '2 tbsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 5, 42, '2 tbsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 5, 43, '1/2 cup');



--ingredients for Club Sandwiches
INSERT INTO ingredients (ingredient_name) VALUES ( 'Bread' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Mayonnaise' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Deli Turkey' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Deli Ham' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Bacon' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Tomato' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Lettuce' )

INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 6, 44, '6 slices');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 6, 45, '4 tbsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 6, 46, '4 oz');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 6, 47, '4 oz');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 6, 43, '2 slices');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 6, 48, '4 slices, cooked');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 6, 49, '1, sliced');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 6, 50, '1/2 cup, rinsed');


--ingredients for Roasted Parmesan Asparagus
INSERT INTO ingredients (ingredient_name) VALUES ( 'Asparagus' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Parmesan Chese' )

INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 7, 51, '1 lb.');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 7, 37, '1 tbsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 7, 12, '1 clove, minced');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 7, 52, '3 tbsp, grated');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 7, 26, '1 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 7, 27, '1 tsp');


--ingredients for Breakfast Burritos 8
INSERT INTO ingredients (ingredient_name) VALUES ( 'Pork Sausage' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Hash Brown Potatoes' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Fresh Salsa' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Flour Tortillas' )

INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 8, 53, '1 lb, spicy or mild');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 8, 54, '3 cups');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 8, 41, '1 tbsp, diced');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 8, 42, '1 tbsp, diced');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 8, 2, '10 large');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 8, 3, '1/4 cup');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 8, 55, '1 cup');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 8, 43, '2 cups');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 8, 56, '8, 12" tortillas');


--ingredients for Grilled Chicken 9
INSERT INTO ingredients (ingredient_name) VALUES ( 'Chicken Breasts' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Dried Oregano' )

INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 9, 37, '1/4 cup');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 9, 35, '1/4 cup');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 9, 12, '3 cloves, minced');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 9, 58, '1 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 9, 23, '1/4 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 9, 26, '1/2 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 9, 27, '1/2 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 9, 57, '4, boneless, skinless; about 2 lbs');


--ingredients for chicken stir fry
INSERT INTO ingredients (ingredient_name) VALUES ( 'Dark Sesame Oil' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Broccoli' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Mushrooms' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Carrots' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Green Beans' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Bok Choy' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Teriyaki Sauce' )

INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 10, 57, '2 lbs., boneless, skinless');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 10, 59, '2 tbsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 10, 12, '2 cloves, minced');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 10, 60, '1 head, stems removed');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 10, 61, '1 dozen, sliced');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 10, 62, '3, peeled and julienned');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 10, 63, '1/4 lb, diced');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 10, 64, '1 head, chopped');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 10, 65, '2-3 tbsp');

--ingredients for buffalo chicken enchiladas
INSERT INTO ingredients (ingredient_name) VALUES ( 'Buffalo Sauce' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Ground Cumin' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Corn Tortillas' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Blue Cheese Crumbles' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Blue Cheese Dressing' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Scallions' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Rotisserie Chicken' )

INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 11, 66, '1 cup');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 11, 39, '3 tbsp, melted');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 11, 72, '4 cups, shredded');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 11, 24, '8 oz, room temp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 11, 43, '2 cups, shredded');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 11, 71, '1 bunch, thinly sliced');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 11, 67, '1/4 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 11, 68, '16');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 11, 69, '2 tbsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 11, 70, '2 tbsp');


--ingredients for chicken noodle soup




--ingredients for quesadilla



--ingredients for deviled eggs
--ingredients for choc chip cookies
--ingredients for creme brulee
--ingredients for apple pie 
--ingredients for spinach artichoke dip




--hummus
--marinated pretzels
--kettle corn
--pigs in a blanket


GO