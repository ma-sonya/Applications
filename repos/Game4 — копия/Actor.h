#pragma once
#include <iostream>

#include "Framework.h"

struct Position
{
	float x;
	float y;
};

class Actor
{
public:
	Actor(Sprite* _sprite, int _x, int _y)
	{
		sprite = _sprite;
		pos.x = _x;
		pos.y = _y;
	}

	Actor(Sprite* _sprite) : sprite(_sprite) {}




public:
	void setPosition(int newX, int newY)
	{
		pos.x = newX;
		pos.y = newY;

	}
	Position getPosition()
	{
		return pos;
	}
	void move(int deltX, int deltY)
	{
		pos.x = pos.x + deltX;
		pos.y = pos.y + deltY;
	}


	virtual ~Actor();

private:
	Sprite* sprite;
	Position pos;
};