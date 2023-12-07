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
	ingredient_name varchar(60) NOT NULL,
	CONSTRAINT PK_ingredient PRIMARY KEY (ingredient_id)
)
CREATE TABLE recipes (
    recipe_id int IDENTITY (1,1) NOT NULL,
	recipe_name varchar(60) NOT NULL,
	recipe_instructions varchar(500) NOT NULL,
	CONSTRAINT PK_recipe PRIMARY KEY (recipe_id)
)


CREATE TABLE users_saved_recipes (
	user_recipe_id int IDENTITY (1,1) NOT NULL,
	user_id int NOT NULL,
	recipe_name varchar(60) NOT NULL,
	recipe_instructions varchar(500) NOT NULL,
	CONSTRAINT PK_user_saved_recipe PRIMARY KEY (user_recipe_id),
	CONSTRAINT FK_user_saved_recipe_users FOREIGN KEY (user_id) REFERENCES users (user_id)
)

CREATE TABLE recipes_ingredients (
    user_recipe_id int NOT NULL,
	ingredient_id int NOT NULL,
	quantity varchar(60) NOT NULL,
	CONSTRAINT PK_recipes_ingredients PRIMARY KEY (user_recipe_id, ingredient_id),
	CONSTRAINT FK_recipes_ingredients_recipes FOREIGN KEY (user_recipe_id) 
	REFERENCES users_saved_recipes (user_recipe_id),
    CONSTRAINT FK_recipes_ingredients_ingredients FOREIGN KEY (ingredient_id) REFERENCES ingredients (ingredient_id)
)

--populate default data
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

INSERT INTO ingredients (ingredient_name) VALUES ( 'Rice' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Eggs' )
INSERT INTO ingredients (ingredient_name) VALUES ( 'Milk' )

INSERT INTO recipes (recipe_name, recipe_instructions ) VALUES ( 'Pizza' , 'Delicious Italian Delicacy')
INSERT INTO recipes (recipe_name, recipe_instructions ) VALUES ( 'Pasta' , 'Carbs')
INSERT INTO recipes (recipe_name, recipe_instructions ) VALUES ( 'Tapioca' , 'yummy yummy')
INSERT INTO recipes (recipe_name, recipe_instructions ) VALUES ( 'Cake' , 'mhhmm mhhmm')

INSERT INTO users_saved_recipes (user_id, recipe_name, recipe_instructions) VALUES (1, 'Pizza', 'Bake for 30 mins');
INSERT INTO users_saved_recipes (user_id, recipe_name, recipe_instructions) VALUES (2, 'Pasta', 'Boil for 25 mins');
INSERT INTO users_saved_recipes (user_id, recipe_name, recipe_instructions) VALUES (2, 'Cake', 'Bake for 1 hour');

INSERT INTO recipes_ingredients (user_recipe_id, ingredient_id, quantity) 
	VALUES ( 1, 2, '1 ea');
INSERT INTO recipes_ingredients (user_recipe_id, ingredient_id, quantity) 
	VALUES ( 1, 1 ,'2 ea');
INSERT INTO recipes_ingredients (user_recipe_id, ingredient_id, quantity) 
	VALUES ( 2, 3, '2 ea');
INSERT INTO recipes_ingredients (user_recipe_id, ingredient_id, quantity) 
	VALUES ( 3, 2, '2 ea');
INSERT INTO recipes_ingredients (user_recipe_id, ingredient_id, quantity) 
	VALUES ( 2, 2, '3 ea');



GO