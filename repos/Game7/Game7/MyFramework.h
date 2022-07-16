#pragma once
#include "Framework.h"
#include <cmath>

class MyFramework : public Framework 
{
private:

	int r_x;
	int r_y;

	Sprite* ship;
	Sprite* asteroid;
	Sprite* aim;
	Sprite* bullet;

	int sh_w, sh_h;
	int as_w, as_h;
	int ai_w, ai_h;
	int bu_w, bu_h;

	int ship_pos_x;
	int ship_pos_y;

	float b_x, b_y;
	float b_dir_x, b_dir_y;
	bool is_bullet_active;

	static const int sNumAsteroids = 256;
	int a_x[sNumAsteroids];
	int a_y[sNumAsteroids];
	bool exist_[sNumAsteroids];

	int recent_mouse_x;
	int recent_mouse_y;

 public:

	 virtual void PreInit(int& width, int& height, bool& fullscreen) override
	 {
		 width = 800;
		 height = 600;
		 fullscreen = false;
	 }

	 virtual bool Init() override 
	 {
		 int sx, sy;
		 getScreenSize(sx, sy);

		 showCursor(false);

		 getSpriteSize(ship, sh_w, sh_h);
		 getSpriteSize(asteroid, as_w, as_h);
		 getSpriteSize(aim, ai_w,ai_h);
		 getSpriteSize(bullet, bu_w, bu_h);

		 for (int i = 0; i < sNumAsteroids; i++)
		 {
			 a_x[i] = rand() % sx;
			 a_y[i] = rand() % sy;
			 exist_[i] = true;
		 }

		 ship_pos_x = sx / 2;
		 ship_pos_y = sy / 2;

		 r_x = ship_pos_x;
		 r_y = ship_pos_y;

		 b_x = b_y = 0;
		 is_bullet_active = false;
		 b_dir_x = b_dir_y = 1;
		 return true;
	 }

	 virtual void Close() override 
	 {
		 destroySprite(ship);
		 destroySprite(asteroid);
		 destroySprite(aim);

	 }

	 virtual bool Tick() override 
	 {
		 drawTestBackground();
		 drawSprite(ship, ship_pos_x - sh_w / 2, ship_pos_y - sh_h / 2);
		 
		 bool should_exit = true;

		 for (int i = 0; i < sNumAsteroids; ++i)
		 {
			 if (exist_[i])
			 {
				 int offset_y = (int)(30 * sinf(getTickCount() * 0.01f + a_x[i]));
				 drawSprite(asteroid, a_x[i] - as_w / 2, a_y[i] - as_w / 2 + offset_y);
				 should_exit = false;
			 }
		 }

		 drawSprite(aim, r_x - ai_w / 2, r_y - ai_h / 2);

		 if (is_bullet_active)
		 {
			 const float speed = 1.5f;

			 b_x = b_x + b_dir_x * speed;
			 b_y = b_y + b_dir_y * speed;

			 int sx, sy;
			 getScreenSize(sx, sy);

			 if (b_x < 0 || b_x > sx)
				 is_bullet_active = false;

			 if (b_y < 0 || b_y > sy)
				 is_bullet_active = false;
		 }

		 if (is_bullet_active)
		 {
			 drawSprite(bullet, b_x - bu_w / 2, b_y - bu_h / 2);
		 }


		 return should_exit;
	 }

	 virtual void onMouseMove(int x, int y, int xrelative, int yrelative) override 
	 {
		 r_x = x;
		 r_y = y;

	 }

	 virtual void onMouseButtonClick(FRMouseButton button, bool isReleased) override 
	 {
		 float rx = (float)r_x;
		 float ry = (float)r_y;
		 if (button == FRMouseButton::LEFT && !isReleased)
		 {
			 for (int i = 0; i < sNumAsteroids; ++i)
			 {
				 if (!exist_[i])
					 continue;

				 float x = a_x[i];
				 float y = a_y[i];

				 float dist_sq = (rx - x) * (rx - x) + (ry - y) * (ry - y);

				 if (dist_sq < 10 * 10)
				 {
					 exist_[i] = false;
					 continue;
				 }
			 }
		 }

		 if (button == FRMouseButton::RIGHT && !isReleased)
		 {
			 b_x = ship_pos_x;
			 b_y = ship_pos_y;
			 b_dir_x = (rx - b_x);
			 b_dir_y = (ry - b_y);

			 //повертаємо до нормального стану
			 float len = sqrtf(b_dir_x * b_dir_x + b_dir_y * b_dir_y);
			 b_dir_x = b_dir_x / len;
			 b_dir_y = b_dir_y / len;

			 is_bullet_active = true;
		 }

	 }

	 virtual void onKeyPressed(FRKey k) override 
	 {
		 onKey(k);
	 }

	 virtual void onKeyReleased(FRKey k) override 
	 {

	 }

	 virtual const char* GetTitle() override
	 {
		 return "asteroids";
	 }

	 private:

		 void onKey(FRKey k)
		 {
			 switch (k)
			 {
			 case FRKey::LEFT:
				 ship_pos_x -= 10;
				 break;
			 case FRKey::RIGHT:
				 ship_pos_x += 10;
				 break;
			 case FRKey::DOWN:
				 ship_pos_y += 10;
				 break;
			 case FRKey::UP:
				 ship_pos_y -= 10;
				 break;
			 }

			 ship_pos_x = ship_pos_x < 0 ? 0 : ship_pos_x;
			 ship_pos_y = ship_pos_y < 0 ? 0 : ship_pos_y;

			 int sx, sy;
			 getScreenSize(sx, sy);

			 ship_pos_x = ship_pos_x > sx ? sx : ship_pos_x;
			 ship_pos_y = ship_pos_y > sy ? sy : ship_pos_y;
		 }



};