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
	recipe_instructions varchar(500) NOT NULL,
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
    CONSTRAINT PK_meal PRIMARY KEY (meal_id),
)

CREATE TABLE meals_recipes (
	meal_id int NOT NULL,
	recipe_id int NOT NULL,
	recipe_name varchar(60),
	CONSTRAINT FK_meals_recipes_meals FOREIGN KEY (meal_id) 
	REFERENCES meals (meal_id),
	CONSTRAINT FK_meals_recipes_recipes FOREIGN KEY (recipe_id) 
	REFERENCES recipes (recipe_id)
)

CREATE TABLE meal_plans (
    meal_plan_id int IDENTITY (1,1) NOT NULL,
	meal_plan_name varchar(60),
    user_id int NOT NULL,
    CONSTRAINT PK_meal_plan PRIMARY KEY (meal_plan_id),
    CONSTRAINT FK_meal_plan_users FOREIGN KEY (user_id) 
	REFERENCES users (user_id)
)

CREATE TABLE meal_plans_meals (
	meal_plan_id int NOT NULL,
    meal_id int NOT NULL,
    meal_name varchar(60),
	CONSTRAINT FK_meal_plan_meal_plans FOREIGN KEY (meal_plan_id) REFERENCES meal_plans (meal_plan_id),
	CONSTRAINT FK_meal_plan_meals FOREIGN KEY (meal_id) REFERENCES meals (meal_id)
)

--populate default data
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

INSERT INTO ingredients (ingredient_name) VALUES ( 'Rice' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Eggs' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Milk' )

INSERT INTO recipes (recipe_name, recipe_instructions ) VALUES ( 'Calzone' , 'Delicious Italian Delicacy')
INSERT INTO recipes (recipe_name, recipe_instructions ) VALUES ( 'Carbonara' , 'Carbs')
INSERT INTO recipes (recipe_name, recipe_instructions ) VALUES ( 'Tapioca' , 'yummy yummy')
INSERT INTO recipes (recipe_name, recipe_instructions ) VALUES ( 'Pastel' , 'mhhmm mhhmm')

INSERT INTO recipes (user_id, recipe_name, recipe_instructions) VALUES (1, 'Pizza', 'Bake for 30 mins');
INSERT INTO recipes (user_id, recipe_name, recipe_instructions) VALUES (2, 'Pasta', 'Boil for 25 mins');
INSERT INTO recipes (user_id, recipe_name, recipe_instructions) VALUES (2, 'Cake', 'Bake for 1 hour');

INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 5, 2, '1 ea');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 5, 1 ,'2 ea');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 2, 3, '2 ea');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 7, 2, '2 ea');
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) 
	VALUES ( 2, 2, '3 ea');

INSERT INTO meals (meal_name, meal_description) VALUES ('Breakfast', 'IDK');
INSERT INTO meals (meal_name, meal_description) VALUES ('Lunch', 'IDK');
INSERT INTO meals (meal_name, meal_description) VALUES ('Dinner', 'IDK');

insert into meals_recipes (meal_id, recipe_id) VALUES (1,1);
insert into meals_recipes (meal_id, recipe_id) VALUES (1,3);
insert into meals_recipes (meal_id, recipe_id) VALUES (2,2);
insert into meals_recipes (meal_id, recipe_id) VALUES (3,1);

INSERT INTO meal_plans (user_id, meal_plan_name) VALUES (1, 'Vegan');  
INSERT INTO meal_plans (user_id, meal_plan_name) VALUES (1, 'Leftovers');  
INSERT INTO meal_plans (user_id, meal_plan_name) VALUES (2, 'Bulk Prep');
INSERT INTO meal_plans (user_id, meal_plan_name) VALUES (2, 'vegetarian');

insert into meal_plans_meals (meal_plan_id, meal_id, meal_name) VALUES (1, 1, 'Breakfast');
insert into meal_plans_meals (meal_plan_id, meal_id, meal_name) VALUES (1, 3, 'Dinner');
insert into meal_plans_meals (meal_plan_id, meal_id, meal_name) VALUES (2, 1, 'Breakfast');


GO