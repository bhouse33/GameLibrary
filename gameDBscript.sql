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
	image			varchar(100)	null,
	description		nvarchar(100)	null,
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

CREATE TABLE games_borrowed
(
	game_id		int			NOT NULL,
	borrowed_id	int			NOT NULL,

	constraint fk_game_id foreign key (game_id) REFERENCES games(id),
	constraint fk_borrowed_id foreign key (borrowed_id) REFERENCES borrowed(id)
);
CREATE TABLE genre
(
	id			int				identity(1,1),
	name		varchar(25)		NOT NULL,

	constraint pk_genre	primary key (id)
);
CREATE TABLE game_genre(
	genre_id		int			NOT NULL,
	game_id			int			NOT NULL,

	constraint fk_genre foreign key(genre_id) references genre(id),
	constraint fk_game foreign key(game_id) references games(id)
);



COMMIT TRANSACTION;
