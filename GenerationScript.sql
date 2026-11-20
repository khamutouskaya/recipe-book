create schema if not exists `recipe_database`;

use `recipe_database`;

create table if not exists `products`(
	`id` int not null auto_increment,
    `metric_type` varchar(50) not null,
    `name` varchar(50) not null,
    `calories` double not null,
    `protein` double not null,
    `fat` double not null,
    `cab` double not null,
     constraint `PK_products` primary key (`id` ASC)
);

create table if not exists `categories`(

	`id` int not null auto_increment,
    `name` varchar(50) not null,
     constraint `PK_categories` primary key (`id` ASC)
);

insert into `categories` (`name`) values ('Zupy');
insert into `categories` (`name`) values ('Dania główne');
insert into `categories` (`name`) values ('Desery');
insert into `categories` (`name`) values ('Przekąski');

create table if not exists `recipes`(
	`id` int not null auto_increment,
    `name` varchar(50) not null,
    `description` varchar(500) not null,
     constraint `PK_recipes` primary key (`id` ASC)
);

create table if not exists `recipe_categories`(

	`id` int not null auto_increment,
    `recipe_id` int not null,
    `category_id` int not null,
     constraint `PK_recipe_categories` primary key (`id` ASC),
     constraint `FK_recipe_categories_recipes` FOREIGN KEY(`recipe_id`) REFERENCES `recipes` (`id`),
     constraint `FK_recipe_categories_categories` FOREIGN KEY(`category_id`) REFERENCES `categories` (`id`)
);

create table if not exists `recipe_products`(

	`id` int not null auto_increment,
    `recipe_id` int not null,
    `product_id` int not null,
    `count` int not null,
     constraint `PK_recipe_products` primary key (`id` ASC),
     constraint `FK_recipe_products_recipes` FOREIGN KEY(`recipe_id`) REFERENCES `recipes` (`id`),
     constraint `FK_recipe_products_products` FOREIGN KEY(`product_id`) REFERENCES `products` (`id`)
);