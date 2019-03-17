USE master;
GO

DROP DATABASE IF EXISTS GameDB
GO

CREATE DATABASE GameDB
GO

USE GameDB
GO

BEGIN TRANSACTION;

CREATE TABLE games
(
	id				int			identity(1,1),
	title			nvarchar(100)	NOT NULL,
	image			nvarchar(2000)	null,
	description		nvarchar(2000)	null,
	min_players		int				null,
	max_players		int				null,
	recommended_age int				null,
	avg_play_time	int				null,
	bgg_weight		float			null,
	bgg_rating		float			null,
	quantity		int				NOT NULL,

	constraint pk_game_id primary key(id)
	);

CREATE TABLE borrowed
(
	id			int			IDENTITY(1,1),
	borrowed	bit			NOT NULL,
	borrowed_by	nvarchar(100) NOT NULL

	constraint pk_borrowed primary key(id)
);

CREATE TABLE game_borrowed
(
	game_id		int			NOT NULL,
	borrowed_id	int			NOT NULL,

	constraint fk_game_id foreign key (game_id) REFERENCES games(id),
	constraint fk_borrowed_id foreign key (borrowed_id) REFERENCES borrowed(id)
);
CREATE TABLE category
(
	id			int				identity(1,1),
	category_name		varchar(25)		NOT NULL,

	constraint pk_genre	primary key (id)
);
CREATE TABLE game_category(
	category_id		int			NOT NULL,
	game_id			int			NOT NULL,

	constraint fk_category foreign key(category_id) references category(id),
	constraint fk_game foreign key(game_id) references games(id)
);
CREATE TABLE mechanic(
	id			int			identity(1,1),
	mechanic_name		varchar(50) NOT NULL,

	constraint pk_mechanic_id primary key (id)
);

CREATE TABLE game_mechanics(
	game_id		int			NOT NULL,
	mechanic_id	int			not null,

	constraint fk_games_mechanics foreign key (game_id) references games(id),
	constraint fk_mechanics foreign key (mechanic_id) references mechanic(id)
);
set identity_insert mechanic ON;

INSERT INTO mechanic (id, mechanic_name)
VALUES (1, 'Acting'),
(2, 'Action Point Allowance System'),
(3, 'Area Control'),
(4, 'Auction/Bidding'),
(5, 'Betting/Wagering'),
(6,'Campaign'),
(7,'Card Drafting'),
(8,'Cooperative Play'),
(9,'Deck Building'),
(10,'Dice Rolling'),
(11,'Grid Movement'),
(12,'Hand Management'),
(13,'Memory'),
(14,'Modular Board'),
(15,'Paper-and-Pencil'),
(16,'Partnerships'),
(17,'Pattern Building'),
(18,'Pattern Recognition'),
(19,'Pick-up and deliver'),
(20,'Player Elimination'),
(21, 'Press Your Luck'),
(22, 'Role Playing'),
(23, 'Roll/spin and Move'),
(24, 'Route/Network Building'),
(25, 'Secret unit deployment'),
(26, 'Set Collection'),
(27, 'Simulation'),
(28, 'Tile Placement'),
(29, 'Trading'),
(30, 'Trick-taking'),
(31, 'Variable Phase Order'),
(32, 'Variable Player Powers'),
(33, 'Voting'),
(34, 'Worker Placement');

set identity_insert mechanic off;

set identity_insert category on;
INSERT INTO category (id, category_name)
VALUES 
(1, 'Abstract Strategy'),
(2, 'Action/Dexterity'),
(3, 'Adventure'),
(4, 'American West'),
(5, 'Aviation / Flight'),
(6, 'Bluffing'),
(7, 'Card game'),
(8, 'Children’s game'),
(9, 'City building'),
(10, 'Civilization'),
(11, 'Deduction'),
(12, 'Dice'),
(13, 'Economic'),
(14, 'Educational'),
(15, 'Electronic'),
(16, 'Expansion for base-game'),
(17, 'Exploration'),
(18, 'Fantasy'),
(19, 'Farming'),
(20, 'Fighting'),
(21, 'Horror'),
(22, 'Humor'),
(23, 'Industry/Manufacturing'),
(24, 'Math'),
(25, 'Maze'),
(26, 'Medical'),
(27, 'Medieval'),
(28, 'Memory'),
(29, 'Miniatures'),
(30, 'Modern Warfare'),
(31, 'Murder/Mystery'),
(32, 'Music'),
(33, 'Mythology'),
(34, 'Nautical'),
(35, 'Negotiation'),
(36, 'Number'),
(37, 'Party Game'),
(38, 'Pirates'),
(39, 'Political'),
(40, 'Puzzle'),
(41, 'Racing'),
(42, 'Real-Time'),
(43, 'Renaissance'),
(44, 'Science Fiction'),
(45, 'Space Exploration'),
(46, 'Spies/Secret Agent'),
(47, 'Sports'),
(48, 'Territory Building'),
(49, 'Trains'),
(50, 'Transportation'),
(51, 'Travel'),
(52, 'Trivia'),
(53, 'Wargame'),
(54, 'Zombies');
set identity_insert category OFF;


SET IDENTITY_INSERT games ON;


insert into games (id, title, [image], [description], min_players, max_players, recommended_age, avg_play_time, bgg_weight, bgg_rating, quantity)
values (1, 'Downforce', 
'https://cf.geekdo-images.com/imagepage/img/ozyfHOhaPip151N368D7PjbUoS8=/fit-in/900x600/filters:no_upscale()/pic3432548.png', 'High-stakes bidding on million-dollar race cars. Frantic bets placed in secret even as the cars race around the track. And to the victor, the biggest purse of all. But in the world of motor racing, the margin between victory and defeat can be a single moment: a steep banked turn, tires screaming and spitting out smoke, and the downforce, pressing you down in your seat and keeping you on the track as you make your move inside to pull ahead. Downforce is a card-driven bidding, racing, and betting game for 2-6 players based on Top Race, the award-winning design by the legendary Wolfgang Kramer. Players first bid to own the six cars in the race, then they play cards from their hand to speed them around the track. However, most cards will also move their opponents'' cars. So figuring out just the right time to play a card is the key to victory. Along the way, players make secret bets on who they think will win the race. Whoever has the most money from their prize money, winning bets, and remaining bank wins.',
 2, 6, 14, 40, 1.84, 7.4, 1)

insert into games (id, title, [image], [description], min_players, max_players, recommended_age, avg_play_time, bgg_weight, bgg_rating, quantity)
 values (2, 'Kingdomino', 
 'https://cf.geekdo-images.com/imagepage/img/VRrk_VIOKzWM_3tF0kpljj1BWME=/fit-in/900x600/filters:no_upscale()/pic3132685.png', 'In Kingdomino, you are a Lord seeking new lands in which to expand your kingdom. You must explore all the lands, wheat fields, lakes, and mountains in order to spot the best plots. But be careful as some other Lords also covet these lands. Dominoes with a kingdom building twist. Each turn, connect a new domino to your existing kingdom, making sure at least one of its sides connects to a matching terrain type already in play. The game mechanics for obtaining the tiles is clever: the order of who picks first depends on which tile was previously chosen. Make sure to secure tiles with crowns- these royal treasures help to multiply the worth of your kingdom at the end of the game! The game ends when each player has completed a 5x5 grid, and then points are counted based on number of connecting tiles and crowns.',
 2, 4, 8, 15, 1.21, 7.4, 1)

 insert into games (id, title, [image], [description], min_players, max_players, recommended_age, avg_play_time, bgg_weight, bgg_rating, quantity)
 values (3, 'Scoville', 
 'https://cf.geekdo-images.com/imagepage/img/8v_PdmeT00gJK5KxY-3fLQ8pmpM=/fit-in/900x600/filters:no_upscale()/pic1903464.jpg', 'The town of Scoville likes it hot! Very hot! That means they love their peppers – but they''re too busy eating them to grow the peppers themselves. That''s where you come in. You''ve been hired by the town of Scoville to meet their need for heat. Your role as an employee of Scoville is to crossbreed peppers to create the hottest new breeds. You''ll have to manage the auctioning, planting, and harvesting of peppers, then you''ll be able to help the town by fulfilling their orders and creating new pepper breeds. Help make the town of Scoville a booming success! Let''s get planting! A round of Scoville consists of a blind auction, which determines player order, a planting phase, a harvesting phase, and a fulfillment phase. Each round, the players plant peppers in the fields. Throughout the game, the available opportunities for crossbreeding increase as more peppers are planted. When harvesting, players move their pawn through the fields, and whenever they move between two planted fields, they harvest peppers. If, for example, they harvest between fields of red and yellow peppers, they crossbreed those and harvest an orange pepper. Harvested peppers are then used to fulfill the town''s peppery desires!',
 2, 4, 13, 75, 2.78, 7.2, 1)

set identity_insert games off;



insert into game_mechanics (game_id, mechanic_id) 
values 
(1, 4),
(1,5),
(1, 12),
(1,32),
(2, 7),
(2, 17),
(2, 28),
(3, 4),
(3, 11),
(3, 26);


insert into game_category (game_id, category_id)
values
(1, 41),
(2, 9),
(2, 18),
(2, 27),
(2, 48),
(3, 19);

COMMIT TRANSACTION;


select * from game_category

select * from game_mechanics