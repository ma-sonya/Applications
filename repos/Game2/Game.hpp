#pragma once

#include "Framework.h"

/* Test Framework realization */
class MyFramework : public Framework {

public:

	virtual void PreInit(int& width, int& height, bool& fullscreen)
	{
		width = 1000;
		height = 1000;
		fullscreen = false;
	}

	virtual bool Init()
	{

		return 1;
	}

	virtual void Close()
	{
		destroySprite()


	}

	virtual bool Tick() {
		drawTestBackground();
		return false;
	}

	virtual void onMouseMove(int x, int y, int xrelative, int yrelative) {

	}

	virtual void onMouseButtonClick(FRMouseButton button, bool isReleased) {

	}

	virtual void onKeyPressed(FRKey k) {
	}

	virtual void onKeyReleased(FRKey k) {
	}

	virtual const char* GetTitle() override
	{
		return "asteroids";
	}
};