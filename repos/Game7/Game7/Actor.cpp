#include <cmath>
#include "Actor.h"

const float Actor::speedStep = 0.01f;
const float Actor::maxSpeed = 2.75f;

float Actor::speedX = 0.0f;
float Actor::speedY = 0.0f;

ShootingDirectionManager& Actor::shootingManager = ShootingDirectionManager::Instance();

Actor::Actor(Sprite* s) : _sprite(s), _constSpeedX(0.0f), _constSpeedY(0.0f) {
    if (s)
        getSpriteSize(s, _width, _height);
}

Actor::Actor(Sprite* s, float constSpeedX, float constSpeedY, float x, float y)
    : _sprite(s), _constSpeedX(constSpeedX), _constSpeedY(constSpeedY), _x(x), _y(y) {
    if (s)
        getSpriteSize(s, _width, _height);
}

Actor::~Actor() {}

void Actor::updateSpeed() {
    if (shootingManager[FRKey::LEFT]) {
        if (speedX > -maxSpeed)
            speedX += -speedStep;
    }
    else {
        if (speedX < 0.0f) {
            speedX -= -speedStep;
            speedX = speedX > 0.0f ? 0.0f : speedX;
        }
    }
    if (shootingManager[FRKey::RIGHT]) {
        if (speedX < maxSpeed)
            speedX += speedStep;
    }
    else {
        if (speedX > 0.0f) {
            speedX -= speedStep;
            speedX = speedX < 0.0f ? 0.0f : speedX;
        }
    }
    if (shootingManager[FRKey::UP]) {
        if (speedY > -maxSpeed)
            speedY += -speedStep;
    }
    else {
        if (speedY < 0.0f) {
            speedY -= -speedStep;
            speedY = speedY > 0.0f ? 0.0f : speedY;
        }
    }
    if (shootingManager[FRKey::DOWN]) {
        if (speedY < maxSpeed)
            speedY += speedStep;
    }
    else {
        if (speedY > 0.0f) {
            speedY -= speedStep;
            speedY = speedY < 0.0f ? 0.0f : speedY;
        }
    }
}

void Actor::draw() const {
    drawSprite(_sprite, _x, _y);
}

void Actor::drawCentered() const {
    drawSprite(_sprite, _x - _width / 2, _y - _height / 2);
}

void Actor::move() {
    _x += speedX + _constSpeedX;
    _y += speedY + _constSpeedY;
}

void Actor::moveBack() {
    _x -= speedX + _constSpeedX;
    _y -= speedY + _constSpeedY;
}

float Actor::distance(const Actor& a) const {
    return distance(a.x(), a.y(), a.width(), a.height());
}

float Actor::distance(float x, float y, int width, int height) const {
    float x1 = _x + _width / 2;
    float y1 = _y + _height / 2;
    float x2 = x + width / 2;
    float y2 = y + height / 2;
    return pow(x1 - x2, 2.0f) + pow(y1 - y2, 2.0f);
}

bool Actor::intersect(const Actor& e) const 
{
    return intersect(e.x(), e.y(), e.width(), e.height());
}

bool Actor::intersect(float x, float y, int width, int height) const 
{

    return distance(x, y, width, height) <= pow((_height + height) / 2, 2);
}

void Actor::setSprite(Sprite* s) {
    _sprite = s;
    getSpriteSize(s, _width, _height);
}

