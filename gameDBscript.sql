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

COMMIT TRANSACTION;

