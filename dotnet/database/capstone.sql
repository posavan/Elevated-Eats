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
	calories int NOT NULL,
	CONSTRAINT PK_ingredient PRIMARY KEY (ingredient_id)
)
CREATE TABLE recipes (
    recipe_id int IDENTITY (1,1) NOT NULL,
	recipe_name varchar(60) NOT NULL,
	recipe_description varchar(500) NOT NULL,
	CONSTRAINT PK_recipe PRIMARY KEY (recipe_id)
)

CREATE TABLE recipes_ingredients (
    recipe_id int NOT NULL,
	ingredient_id int NOT NULL,
	quantity varchar(60) NOT NULL,
	CONSTRAINT PK_recipes_ingredients PRIMARY KEY (recipe_id, ingredient_id),
	CONSTRAINT FK_recipes_ingredients_recipes FOREIGN KEY (recipe_id) REFERENCES recipes (recipe_id),
    CONSTRAINT FK_recipes_ingredients_ingredients FOREIGN KEY (ingredient_id) REFERENCES ingredients (ingredient_id)


)

CREATE TABLE users_recipes (
	user_id int NOT NULL,
	recipe_id int NOT NULL,
	CONSTRAINT PK_users_recipes PRIMARY KEY (user_id, recipe_id),
	CONSTRAINT FK_users_recipes_users FOREIGN KEY (user_id) REFERENCES users (user_id),
	CONSTRAINT FK_users_recipes_recipes FOREIGN KEY (recipe_id) REFERENCES recipes (recipe_id)

)

--populate default data
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

INSERT INTO ingredients (ingredient_name, calories) VALUES ( 'Rice', 80 )
INSERT INTO ingredients (ingredient_name, calories) VALUES ( 'Eggs', 150 )
INSERT INTO ingredients (ingredient_name, calories ) VALUES ( 'Milk' , 100  )

INSERT INTO recipes (recipe_name, recipe_description ) VALUES ( 'Pizza' , 'Delicious Italian Delicacy')
INSERT INTO recipes (recipe_name, recipe_description ) VALUES ( 'Pasta' , 'Carbs')
INSERT INTO recipes (recipe_name, recipe_description ) VALUES ( 'Tapioca' , 'yummy yummy')
INSERT INTO recipes (recipe_name, recipe_description ) VALUES ( 'Cake' , 'mhhmm mhhmm')

INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) VALUES ( 1, 2, '1 ea')
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) VALUES ( 1, 1 ,'2 ea')
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) VALUES ( 2, 3, '2 ea')
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) VALUES ( 3, 2, '2 ea')
INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) VALUES ( 4, 2, '2 ea')

INSERT INTO users_recipes (user_id, recipe_id) VALUES (1, 1)
INSERT INTO users_recipes (user_id, recipe_id) VALUES (1, 2)

GO