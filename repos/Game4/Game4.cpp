#include <iostream>
#include "Framework.h"


/* Test Framework realization */
class MyFramework : public Framework {

public:

	
	
	

	



	virtual void PreInit(int& width, int& height, bool& fullscreen) override
	{
		width = 300;
		height = 300;
		fullscreen = false;
	}

	virtual bool Init() override 
	{
		Sprite* fon;
		fon = createSprite("C:/Us/Sophie/source/repos/Game4/data/fon.jpg");
		drawSprite(fon, 0, 0);


		return true;
	}

	virtual void Close() override 
	{



	}

	virtual bool Tick() override 
	{

		Sprite* ship;
		ship = createSprite("C:/Users/Sophie/source/repos/Game4/data/spaceship.png");
		drawSprite(ship, 100, 100);
		drawTestBackground();
		return false;

	}

	virtual void onMouseMove(int x, int y, int xrelative, int yrelative)override
	{

	}

	virtual void onMouseButtonClick(FRMouseButton button, bool isReleased)override 
	{
		if (button == FRMouseButton::LEFT)
		{

		}
	}

	virtual void onKeyPressed(FRKey k) override 
	{

	}

	virtual void onKeyReleased(FRKey k) override 
	{

	}

	virtual const char* GetTitle() override
	{
		return "asteroids";
	}
};

int main(int argc, char* argv[])
{

	return run(new MyFramework);
}