#pragma once
#include "Framework.h"

//створимо клас Actor, в якому оголосимо та реалізуємо необхідні для руху та взаємодії Sprites

struct ShootingDirectionManager {
    bool& operator[](FRKey k) {
        switch (k) {
        case FRKey::LEFT:
            return bLeftKeyPressed;
        case FRKey::RIGHT:
            return bRightKeyPressed;
        case FRKey::UP:
            return bUpKeyPressed;
        case FRKey::DOWN:
            return bDownKeyPressed;
        default:
            throw k;
        }
    }

    static ShootingDirectionManager& Instance() {
        static ShootingDirectionManager Instance;
        return Instance;
    }

private:
    ShootingDirectionManager() {}
    ShootingDirectionManager(const ShootingDirectionManager&) = delete;
    ShootingDirectionManager& operator=(const ShootingDirectionManager&) = delete;

    bool bLeftKeyPressed = false;
    bool bRightKeyPressed = false;
    bool bUpKeyPressed = false;
    bool bDownKeyPressed = false;
};

class Actor {

    Sprite* _sprite;
    float _x, _y;
    float _constSpeedX, _constSpeedY;
    int _width, _height;

public:
    static float speedX, speedY;
    static const float speedStep;
    static const float maxSpeed;
    static ShootingDirectionManager& shootingManager;

public:
    Actor(Sprite* s);

    Actor(Sprite* s, float constSpeedX, float constSpeedY, float x, float y);

    ~Actor();

public:
    int width() const { return _width; }
    int height() const { return _height; }
    static void updateSpeed();
    void draw() const;
    void drawCentered() const;
    void move();
    void moveBack();
    void sendBack();
    void setSprite(Sprite* s);
    Sprite* const getSprite() const { return _sprite; }
    const float& constSpeedX() const { return _constSpeedX; }
    const float& constSpeedY() const { return _constSpeedY; }
    float& x() { return _x; }
    float& y() { return _y; }
    const float& x() const { return _x; }
    const float& y() const { return _y; }
    float& constSpeedX() { return _constSpeedX; }
    float& constSpeedY() { return _constSpeedY; }
    float distance(const Actor& e) const;
    float distance(float x, float y, int width, int height) const;
    bool intersect(const Actor& e) const;
    bool intersect(float x, float y, int width, int height) const;
};
