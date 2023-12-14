USE master
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
	recipe_image varchar(8000),
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
	CONSTRAINT PK_recipes_ingredients PRIMARY KEY (recipe_id, ingredient_id, quantity),
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
	meal_plan_image varchar(500),
    user_id int NOT NULL,
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


INSERT INTO recipes (user_id, recipe_name, recipe_instructions, recipe_image ) VALUES ( 1, 'Homemade Lasagna' , '1. Preheat the oven to 350°F. In a large pot of salted water, boil lasagna noodles until al dente according to package directions. Drain, rinse under cold water, and set aside. 
2. In a large skillet or dutch oven, brown beef, sausage, onion, and garlic over medium-high heat until no pink remains. Drain any fat.
3. Stir in the pasta sauce, tomato paste, Italian seasoning, ½ teaspoon of salt, and ¼ teaspoon of black pepper. Simmer uncovered over medium heat for 5 minutes or until thickened.
4. In a separate bowl, combine 1 ½ cups mozzarella, ¼ cup parmesan cheese, ricotta, parsley, egg, and ¼ teaspoon salt.
5. Spread 1 cup of the meat sauce in a 9x13 pan or casserole dish. Top it with 3 lasagna noodles. Layer with 1/3 of the ricotta cheese mixture and 1 cup of meat sauce. Repeat twice more. Finish with 3 noodles topped with remaining sauce.
6. Cover with foil and bake for 45 minutes. 
7. Remove the foil and sprinkle with the remaining 2 ½ cups mozzarella cheese and ¼ cup parmesan cheese. Bake for an additional 15 minutes or until browned and bubbly. Broil for 2-3 minutes if desired.
8. Rest for at least 15 minutes before cutting.', '/img/Lasagna.jpg')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions, recipe_image ) VALUES ( 1, 'Mexican Corn Dip' , '1. In a slow cooker, mix together the corn, rotel, cream cheese, shredded cheese, green onions, garlic, and chili powder. 2. Cook on high for an hour or on low for a few hours.', '/img/corn.jpg')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions, recipe_image ) VALUES ( 1, 'Homemade Mozzarella Sticks' , '1. Line a rimmed baking sheet with parchment paper, set aside. 
2. In a small shallow bowl, mix the flour with salt, pepper, and garlic powder.
3. In a second bowl, add eggs along with 1 tbsp of water and beat until smooth.
4. In a third bowl, mix together the panko and the italian seasoning.
5. Working with one piece of cheese at a time, dip the cheese into the egg mixture, followed by the flour mixture, back into the egg mixture and finally into the panko, gently pressing the panko to ensure it sticks. Set the cheese sticks on the prepared baking sheet and repeat with remaining cheese. 
Place the baking sheet in the freezer and freeze the cheese sticks for 45 minutes.
6. In a large pot, set over medium high heat, bring the oil to 350°F, using a deep fat thermometer. Remove the cheese sticks from the freezer. Working in small batches gently drop them into the hot oil. Cook until golden brown on all sides, about 2 minutes. 
Remove from oil and place on a metal cooling rack set over a baking sheet. Repeat with remaining cheese sticks. 
7. Serve mozzarella sticks immediately with marinara sauce.', '/img/sticks.jpg')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions, recipe_image ) VALUES ( 1, 'Caesar Salad' , 'Dressing: In a small bowl, whisk together garlic, dijon, worcestershire, lemon juice, and red wine vinegar. Slowly drizzle in EVOO while whisking constantly. Season to taste. 
Salad: Rinse, dry, and chop the romaine into bite sized pieces. Place in a large serving bowl and sprinkle generously with shredded parmesan and croutons. Drizzle with caesar dressing and toss gently until lettuce is evenly coated.', '/img/salad.jpeg' )
INSERT INTO recipes (user_id, recipe_name, recipe_instructions, recipe_image ) VALUES ( 1, 'Denver Omelet', '1. Beat eggs, milk and salt & pepper with a fork. Set aside.
2. Melt butter in a 6” skillet over medium heat. Add onion and cook until slightly softened about 3 minutes.
3. Add ham and peppers. Cook an additional 3-5 minute or until peppers are tender.
4. Turn heat to medium-low and add egg mixture. Allow to cook about 2 minutes.
5. Run the spatula along the edges of the egg mixture while lifting the pan to allow raw egg to run underneath the cooked eggs.
6. Once the eggs are almost set, add cheese and cover. Cook until top is set and cheese is melted, about 5-6 minutes.
7. Fold in half and serve.' , '/img/omlete.png')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions, recipe_image ) VALUES ( 1, 'Club Sandwiches' , '1. Arrange 2 slices of bread on a cutting board. Spread each slice with mayonnaise.
2.  Top with turkey, tomato slices and cheddar cheese.
3. Spread mayonnaise on 2 more pieces of bread and place on the cheddar cheese.
4. Top with ham, bacon and lettuce. Spread mayonnaise on the final 2 slices of bread and place on top.
5. Cut sandwich into halves or quarters. Serve with pickles.', '/img/clubSandwich.png')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions, recipe_image ) VALUES (1, 'Roasted Parmesan Asparagus','1. Preheat the oven to 425°F.
2. Rinse asparagus and pat dry. Break off the woody ends.
3. Toss asparagus with olive oil, garlic, salt and pepper and place on a baking pan.
4. Roast 6-10 minutes or until tender-crisp. Add parmesan cheese and broil 1 min.', '/img/asparagus.png')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions, recipe_image ) VALUES (1, 'Breakfast Burritos', '1. Heat a 12" skillet over medium-high heat and add breakfast sausage, crumbling while cooking. Once browned, spoon onto a paper towel-lined plate leaving about 2 tablespoons of fat in the pan.
2. Add frozen hash browns (and bell peppers if using) to the pan and cook for about 10 minutes or until fully cooked and lightly browned.
3. In a small bowl combine eggs, milk and salt & black to taste. Whisk until fully combined.
4. Remove hash browns from the pan and set aside. Add 1 teaspoon of oil to the pan if needed and reduce heat to medium low.
5. Add eggs and cook until just about set, they will continue cooking as they cool.
6. Arrange 8 pieces of foil with a flour tortilla in the center of each. Divide hash brown mixture, eggs, cheese and salsa over tortillas.
7. Enjoy immediately or wrap the burritos in the foil and store it in a plastic zip-top bag in the freezer.', '/img/burrito.jpg')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions, recipe_image ) VALUES (1, 'Grilled Chicken','1. Combine the olive oil, lemon juice, garlic, oregano, chili powder, salt and pepper in a small bowl. Whisk until well combined.
2. Place the chicken breasts in a zip-top plastic bag and pound them using a meat mallet or rolling pin until they are an even thickness, about ½-inch thick.
3. Pour the marinade into the bag with the chicken, seal the bag, and place it in a baking dish (in case of leaks). Turn the bag to coat all parts of the chicken. Place chicken in the refrigerator for 30 minutes to 2 hours.
4. Clean the grates of a gas or charcoal grill (or use an indoor grill pan). Oil the grill grates and heat the grill to medium high (375-450 degrees F).
5. Remove the chicken from the marinade and discard extra marinade. Grill chicken for 5-8 minutes per side, until cooked through. The internal temperature of the chicken should reach 165 degrees F. For the best grill marks and juicy chicken, begin cooking the chicken on a hotter part of the grill for a few minutes and then move it to lower heat to finish cooking through.
6. Let chicken rest for 5 minutes before serving.', '/img/grilledChicken.jpg')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions, recipe_image) VALUES (1, 'Chicken Stir-Fry' , '1. Heat 1 tablespoon oil in a saute pan over medium heat. Add garlic and stir. Place the chicken in the pan and brown 4 minutes on each side. Remove from pan, slice into strips, set aside.
2. Heat remaining tablespoon of oil in a wok over high heat. Add the vegetables and teriyaki sauce. Stir-fry quickly until the vegetables begin to soften. Add the chicken strips, combine well and continue to cook for 2 to 3 minutes. Serve immediately.', '/img/stirFry.jpg')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions, recipe_image) VALUES (1, 'Buffalo Chicken Enchiladas' , 'Preheat the oven to 400 degrees F. Butter a 9-by-13-inch baking dish.
2. Mix the chicken, cream cheese, 1 cup of the Cheddar, 1/3 cup of the hot sauce, white parts of the scallions and cumin in a large bowl until well combined. Stir together the butter, remaining 2/3 cup hot sauce and 3 tablespoons water in a medium bowl. 
3. Microwave the tortillas in batches until warm, softened and foldable, about 30 seconds. Keep warm between damp paper towels. 
4.Spoon a portion of the chicken mixture down the middle of each tortilla and roll up. Place them side by side, seam-side down, in the prepared pan. Pour the hot sauce mixture over the tortillas. Sprinkle with the remaining 1 cup Cheddar and the blue cheese and bake until the cheese is melted and bubbly, 15 to 17 minutes. 
5. Drizzle the blue cheese dressing over the enchiladas and sprinkle with the scallion greens. Serve with more hot sauce.' , '/img/enchaldas.png')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions, recipe_image) VALUES (1, 'Chicken Noodle Soup', '1. Place a large pot over moderate heat and add extra-virgin olive oil. Work close to the stove and add vegetables to the pot as you chop, in the order they are listed.
2. Add bay leaves and season vegetables with salt and pepper, to taste. Add stock to the pot and raise flame to bring liquid to a boil. Add diced chicken tenderloins, return soup to a boil, and reduce heat back to moderate. Cook chicken 2 minutes and add noodles. Cook soup an additional 6 minutes or until noodles are tender and remove soup from the heat.
3. Stir in parsley and dill, remove bay leaves and serve. This is a thick soup. Add up to 2 cups of water if you like chicken soup with lots of broth.', '/img/chickenNoodle.png')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions, recipe_image) VALUES (1, 'Crispy Smashed Potatoes', '1. Cook potatoes: Bring a pot of water to the boil, add 1 tbsp salt. Cook potatoes until soft - small ones should take around 20 to 25 minutes, medium ones might take 30 minutes. Its ok if the skin splits. Alternatively, steam or microwave them.
2. Preheat oven to 390°F.
3. Steam dry: Drain the potatoes and let them dry in the colander for 5 minutes or so.
4. Smash! Place on the tray then use a large fork or potato masher to squish them, keeping them in one piece. Thin = crisper. Thicker = fluffier insides. (Both good!) More nubbly surface = better crunch!
5. Leave on the tray to steam dry for 5 minutes or so - makes them crispier!
6. Drizzle with butter, then olive oil. Sprinkle with salt and pepper.
7. Bake for 45 minutes to 55 minutes or until deep golden and crispy. Do not flip!
8. Serve hot, sprinkled with parsley if desired.', '/img/potatoes.png')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions, recipe_image) VALUES (1, 'Marinated Pretzels', '1. Pour the pretzels into a large bowl.
2. Blend olive oil, ranch-style dressing mix, garlic powder, and dill weed in a medium bowl; pour the mixture over pretzels. Let pretzels marinate for 1 hour, tossing approximately every 10 minutes.
3. Preheat oven to 350°F.
4. Spread marinated pretzels on a large cookie sheet. Bake in the preheated oven for 8 to 10 minutes. Allow the pretzels to cool, then serve.', 
'/img/pretzels.png')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions, recipe_image) VALUES (1, 'Kettle Corn', '1. Heat vegetable oil in a large pot over medium heat. Stir in popcorn kernels and sugar.
2. Cover and shake the pot constantly to prevent sugar from burning. When popping has slowed to once every 2 to 3 seconds, remove the pot from the heat and shake for a few minutes until popping stops.
3. Pour popcorn into a large bowl and allow to cool, stirring occasionally to break up large clumps.', '/img/popcorn.jpg')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions, recipe_image) VALUES (1, 'Chocolate Chip Cookies', '1. Preheat oven to 375 degrees F. Line three baking sheets with parchment paper and set aside.
2. In a medium bowl mix flour, baking soda, baking powder and salt. Set aside.
3. Cream together butter and sugars until combined.
4. Beat in eggs and vanilla until light (about 1 minute).
5. Mix in the dry ingredients until combined.
6. Add chocolate chips and mix well.
7. Roll 2-3 Tablespoons (depending on how large you like your cookies) of dough at a time into balls and place them evenly spaced on your prepared cookie sheets.
8. Bake in preheated oven for approximately 8-10 minutes. Take them out when they are just barely starting to turn brown.
9. Let them sit on the baking pan for 2 minutes before removing to cooling rack.', '/img/cookies.png')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions, recipe_image) VALUES (1, 'Dutch Apple Pie', '1. Make the Pie Crust: Process ¾ cups of the flour, the sugar, and salt together in a food processor until combined, about 2 one-second pulses. Add the butter and shortening and process until a homogenous dough just starts to collect in uneven clumps, about 7 to 10 seconds (the dough will resemble cottage cheese curds with some very small pieces of butter remaining, but there should be no uncoated flour). Scrape down the sides and bottom of the bowl with a rubber spatula and redistribute the dough evenly around the bowl. Add the remaining ½ cup flour and pulse until the mixture is evenly distributed around the bowl and the mass of dough has been broken up, 4 to 6 quick pulses. Empty the mixture into a medium bowl.
2. Sprinkle the vodka and water over the mixture. With a rubber spatula, use a folding motion to mix, pressing down on the dough until it is slightly tacky and sticks together. Turn the dough out onto a lightly floured work surface and bring it together with your hands, pressing it into a 6-inch round. Lightly flour the top and gently and quickly roll it out to a 13-inch circle, picking it up and doing a quarter turn after every couple of rolls to keep it from sticking.
3. Transfer the dough to a 9-inch pie plate and gently press it into the bottom and up the sides. Trim the dough to 1 inch beyond the lip of the pie plate, then tuck it under itself so it is flush with the edge of the pie plate. Flute the edges or press with the tines of a fork, then refrigerate the dough-lined plate for at least 2 hours.
4. Blind Bake the Pie Crust: Preheat oven to 350 degrees F. Line the chilled pie dough with aluminum foil and use granulated sugar to fill the whole pie plate. Bake for 40 minutes; remove the foil and sugar and place the crust on a wire rack while you make the filling.
5. Increase the oven temperature to 425 degrees F.
6. Make the Apple Filling: Peel, quarter, and core the apples; slice each quarter crosswise into pieces ¼ inch thick. Toss the apples, sugar, cinnamon, and salt in a large bowl to combine. Heat the butter in a large Dutch oven (or pot) over high heat until foaming subsides; add the apples and toss to coat. Reduce the hat to medium-high and cook, covered, stirring occasionally, until the Granny Smith apple slices are tender and the McIntosh apple slices are softened and beginning to break down, about 10 minutes.
7. Set a large colander over a large bowl; transfer the cooked apples to the colander. Shake the colander and toss the apples to drain off as much juice as possible. Bring the drained juice and the cream to a boil in the now-empty Dutch oven over high heat; cook, stirring occasionally, until thickened and a wooden spoon leaves a trail in the mixture, about 5 minutes. Transfer the apples to the prebaked pie shell; pour the reduced juice mixture over and smooth with a rubber spatula.
8. Make the Streusel Topping: Combine the flour and sugars in a medium bowl; drizzle with the melted butter and toss with a fork until evenly moistened and the mixture forms many large chunks with pea-sized pieces mixed throughout. Sprinkle the streusel evenly over the pie filling.
9. Bake the Pie: Set the pie plate on a baking sheet and bake until the streusel topping is deep golden brown, 10 to 20 minutes. Cool on a wire rack to room temperature and serve.'
, '/img/pie.png')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions, recipe_image) VALUES (1, 'Veggie Quesadillas' , '1. Heat 1 tablespoon oil in a medium pan over medium-high heat. Add the oil, bell peppers, black beans, corn, onion, garlic, cumin, chili powder, salt, and pepper. Season with salt and pepper and cook for 3-4 minutes or until the bell peppers and onions of softened. Turn off heat and stir in the cilantro.
2. In a clean skillet over medium heat, add a flour tortilla. Top with cheese, cooked veggies mixture, and another layer of cheese. Place another tortilla on top and cook, flipping once, until golden on both sides, about 3 minutes per side. Repeat with remaining ingredients. Slice and Serve!', '/img/quesadilla.jpg')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions, recipe_image) VALUES (1, 'Malawah', '1. Beat or whisk all ingredients together in a bowl. Alternatively, mix together with a hand-held blender.
2. Heat up a frying pan with enough butter or oil to lightly coat surface.
3. Ladle some batter into pan and swirl such that batter distributes to make a thin layer which spreads towards curving sides of pan
4. Fry for about a minute or until lightly golden, then flip over and fry another minute or so on the other side.
5. Serve malawah by spreading melted butter and honey on top or sprinkling some sugar on top.', '/img/malawah.webp')
INSERT INTO recipes (user_id, recipe_name, recipe_instructions, recipe_image) VALUES (1, 'Vanilla Creme Brulee', '1. Heat oven to 325 degrees. In a saucepan, combine cream, vanilla, and salt; cook over low heat just until hot. Let sit for a few minutes.
2. In a bowl, beat yolks and sugar together until light. Stir about a quarter of the vanilla cream into this mixture, then pour sugar-egg mixture into cream and stir. Pour into four 6-ounce ramekins and place ramekins in a baking dish; fill dish with boiling water halfway up the sides of the ramekins.
3. Bake for 30 to 40 minutes, or until centers are barely set. Cool completely. Refrigerate for several hours and up to a couple of days.
4. When ready to serve, top each custard with about a teaspoon of sugar in a thin layer.
5. Place ramekins in a broiler 2 to 3 inches from heat source. Turn on broiler. Cook until sugar melts and browns or even blackens a bit, about 5 minutes. Serve within 2 hours.', '/img/cremeBrulee.jpg')




INSERT INTO meals (meal_name, meal_description) VALUES ('Appetizers', 'Bite-Sized Beginnings');
INSERT INTO meals (meal_name, meal_description) VALUES ('Breakfast', 'Sunrise Sustenance');
INSERT INTO meals (meal_name, meal_description) VALUES ('Lunch', 'Mid-day Munching');
INSERT INTO meals (meal_name, meal_description) VALUES ('Dinner', 'Nighttime Nourishment');
INSERT INTO meals (meal_name, meal_description) VALUES ('Snacks', 'Tasty Treats');
INSERT INTO meals (meal_name, meal_description) VALUES ('Desserts', 'Feast Finale');


insert into meals_recipes (meal_id, recipe_id) VALUES (1,3);
insert into meals_recipes (meal_id, recipe_id) VALUES (1,2);
insert into meals_recipes (meal_id, recipe_id) VALUES (2,5);
insert into meals_recipes (meal_id, recipe_id) VALUES (2,8);
insert into meals_recipes (meal_id, recipe_id) VALUES (2,19);
insert into meals_recipes (meal_id, recipe_id) VALUES (3,4);
insert into meals_recipes (meal_id, recipe_id) VALUES (3,12);
insert into meals_recipes (meal_id, recipe_id) VALUES (3,18);
insert into meals_recipes (meal_id, recipe_id) VALUES (3,6);
insert into meals_recipes (meal_id, recipe_id) VALUES (4,1);
insert into meals_recipes (meal_id, recipe_id) VALUES (4,10);
insert into meals_recipes (meal_id, recipe_id) VALUES (4,9);
insert into meals_recipes (meal_id, recipe_id) VALUES (4,13);
insert into meals_recipes (meal_id, recipe_id) VALUES (4,7);
insert into meals_recipes (meal_id, recipe_id) VALUES (5,15);
insert into meals_recipes (meal_id, recipe_id) VALUES (5,14);
insert into meals_recipes (meal_id, recipe_id) VALUES (6,17);
insert into meals_recipes (meal_id, recipe_id) VALUES (6,16);
insert into meals_recipes (meal_id, recipe_id) VALUES (6,20);


INSERT INTO meal_plans (user_id, meal_plan_name, meal_plan_description) VALUES (1, 'Vegetarian', 'All the flavor, none of the meat!');  
INSERT INTO meal_plans (user_id, meal_plan_name, meal_plan_description) VALUES (1, '30-Minute Meals', 'Time is of the Essence!');  
INSERT INTO meal_plans (user_id, meal_plan_name, meal_plan_description) VALUES (1, 'Soup and Salad', 'Sorry, no breadsticks!');
INSERT INTO meal_plans (user_id, meal_plan_name, meal_plan_description) VALUES (1, 'Party Plates!', 'Impress guests with a delicious spread');

--insert into meal_plans_meals (meal_plan_id, meal_id, meal_name) VALUES (1, 1, 'Breakfast');
--insert into meal_plans_meals (meal_plan_id, meal_id, meal_name) VALUES (1, 3, 'Dinner');
--insert into meal_plans_meals (meal_plan_id, meal_id, meal_name) VALUES (2, 1, 'Breakfast');

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
INSERT INTO ingredients (ingredient_name) VALUES ('Parsnips')
INSERT INTO ingredients (ingredient_name) VALUES ('Celery')
INSERT INTO ingredients (ingredient_name) VALUES ('Bay Leaves')
INSERT INTO ingredients (ingredient_name) VALUES ('Chicken Stock')
INSERT INTO ingredients (ingredient_name) VALUES ('Chicken Breast Tenders')
INSERT INTO ingredients (ingredient_name) VALUES ('Wide Egg Noodles')
INSERT INTO ingredients (ingredient_name) VALUES ('Fresh Dill')

INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 12, 37, '2 tbsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 12, 62, '2, peeled and chopped');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 12, 73, '1, peeled and chopped');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 12, 11, '1, chopped');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 12, 74, '2 ribs, chopped');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 12, 75, '2, fresh or dried');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 12, 76, '6 cups');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 12, 77, '1 lb');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 12, 78, '1/2 lb');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 12, 18, 'handful, chopped');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 12, 79, 'handful, chopped');
	

--ingredients for smashed potatoes 13
INSERT INTO ingredients (ingredient_name) VALUES ('Small Potatoes')

INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 13, 80, '1 package, about 12-14 potatoes');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 13, 26, '1 tbsp, for boiling');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 13, 39, '1 tbsp, melted');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 13, 37, '1 tbsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 13, 26, '3/4 tsp, for seasoning');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 13, 27, '1/4 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 13, 18, 'finely chopped for garnish');


--ingredients for marinated pretzels
INSERT INTO ingredients (ingredient_name) VALUES ('Pretzels')
INSERT INTO ingredients (ingredient_name) VALUES ('Dry Ranch Dressing Mix')
INSERT INTO ingredients (ingredient_name) VALUES ('Dried Dill Weed')

INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 14, 81, '1 15oz package');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 14, 37, '3/4 cup');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 14, 82, '1 (1oz) package');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 14, 28, '3 tbsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 14, 83, '3 tbsp');

--ingredients for kettle corn
INSERT INTO ingredients (ingredient_name) VALUES ('Popcorn Kernels')
INSERT INTO ingredients (ingredient_name) VALUES ('Sugar')

INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 15, 32, '1/4 cup');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 15, 84, '1/2 cup');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 15, 85, '1/4 cup');


--ingredients for choc chip cookies
INSERT INTO ingredients (ingredient_name) VALUES ('Light Brown Sugar')
INSERT INTO ingredients (ingredient_name) VALUES ('Vanilla Extract')
INSERT INTO ingredients (ingredient_name) VALUES ('Baking Soda')
INSERT INTO ingredients (ingredient_name) VALUES ('Baking Powder')
INSERT INTO ingredients (ingredient_name) VALUES ('Chocolate Chips')

INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 16, 39, '1 cup, softened');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 16, 85, '1 cup');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 16, 86, '1 cup, packed');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 16, 87, '2 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 16, 2, '2');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 16, 25, '3 cups');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 16, 88, '1 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 16, 89, '1/2 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 16, 26, '1 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 16, 90, '2 cups (14 oz)');


--ingredients for apple pie 
INSERT INTO ingredients (ingredient_name) VALUES ('Vegetable Shortening')
INSERT INTO ingredients (ingredient_name) VALUES ('Vodka')
INSERT INTO ingredients (ingredient_name) VALUES ('Granny Smith Apples')
INSERT INTO ingredients (ingredient_name) VALUES ('McIntosh Apples')
INSERT INTO ingredients (ingredient_name) VALUES ('Ground Cinnamon')
INSERT INTO ingredients (ingredient_name) VALUES ('Heavy Cream')
INSERT INTO ingredients (ingredient_name) VALUES ('Ice Water')

INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 17, 25, '1 1/4 cups, divided (crust)');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 17, 85, '1 tbsp (crust)');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 17, 26, '1/2 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 17, 39, '6 tbsp, cold (crust)');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 17, 91, '1/4 cup, chilled');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 17, 92, '2 tbsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 17, 97, '2 tbsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 17, 93, '5 large apples (~2.5 lbs)');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 17, 94, '4 large apples (~2 lbs)');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 17, 85, '1/4 cup (filling)');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 17, 95, '1/2 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 17, 26, 'Pinch');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 17, 39, '2 tbsp (filling)');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 17, 96, '1/2 cup');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 17, 25, '1 1/4 cups (streusel)');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 17, 86, '1/3 cup (streusel)');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 17, 85, '1/3 cup (streusel)');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 17, 39, '7 tbsp, melted (streusel)');

--ingredients for quesadilla
INSERT INTO ingredients (ingredient_name) VALUES ('Monterey Jack Cheese')
INSERT INTO ingredients (ingredient_name) VALUES ('Cilantro')
INSERT INTO ingredients (ingredient_name) VALUES ('Black Beans')
INSERT INTO ingredients (ingredient_name) VALUES ('Water')

INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 18, 37, '1 tbsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 18, 100, '1 cup, rinsed and drained');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 18, 20, '1/2 cup, drained');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 18, 11, '1/2 cup, diced');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 18, 12, '2 cloves, minced');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 18, 67, '1 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 18, 23, '1 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 18, 99, '1/4 cup, chopped');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 18, 56, '4, medium size');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 18, 98, '2 cups, shredded');


--ingredients for malawah
INSERT INTO ingredients (ingredient_name) VALUES ('Ground Cardamom')
INSERT INTO ingredients (ingredient_name) VALUES ('Ground Ginger')
INSERT INTO ingredients (ingredient_name) VALUES ('Honey')

INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 19, 25, '2 cups');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 19, 3, '2 1/2 cups');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 19, 2, '2');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 19, 85, '1 tbsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 19, 102, '1/2 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 19, 103, '1/2 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 19, 26, 'Pinch');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 19, 39, '2 tbsp, for frying');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 19, 104, 'for garnish');


--ingredients for creme brulee
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 20, 96, '2 cups');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 20, 87, '1 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 20, 26, '1/8 tsp');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 20, 2, '5, just the yolks');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 20, 85, '1/2 cup, plus more for topping');





--ingredients for spinach artichoke dip




--hummus
--pigs in a blanket


GO