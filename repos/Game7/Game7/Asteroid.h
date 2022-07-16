#pragma once
#include <vector>
#include "Framework.h"
#include "Actor.h"

struct Rect {
    int x1y1, x2y1, x1y2, x2y2;
};

struct Timer {
    const float end, step;
    float current;

    Timer(float end, float step) : end(end), step(step), current(end) {}

    void begin() {
        current = 0.0f;
    }

    void reset() {
        current = end;
    }

    void tick() {
        if (ended())
            return;
        current += step;
        if (ended())
            reset();
    }

    bool ended() {
        return current >= end;
    }
};

class Asteroid : public Framework {

    static const int Grid = 4;
    static const int Threshold = 100;

    static const char* Title;
    const int AsteroidNumber;
    const float AbilityChance;

    const int Ammo;
    const int AutoAmmo = 3;

    const bool bFullscreen = false;

    int ScreenWidth;
    int ScreenHeight;

    int MapWidth;
    int MapHeight;

    int DeltaWidth = (MapWidth - ScreenWidth) / 2;
    int DeltaHeight = (MapHeight - ScreenHeight) / 2;

    int GridWidth = MapWidth / Grid;
    int GridHeight = MapHeight / Grid;

    bool bPaused = false;
    bool bGameOver = false;

    bool bAutoShooting = false;
    bool bPowerShield = false;
    bool bHasPowerUp = false;

    int BackgroundSpriteWidth;
    int BackgroundSpriteHeight;

    int BulletSpriteWidth;
    int BulletSpriteHeight;

    Timer ShootingDelay = Timer(1.0f, 0.05f);
    Timer AutoShootingDelay = Timer(1.0f, 0.05f);
    Timer AutoShootingDuration = Timer(1.0f, 0.005f);
    Timer PowerShieldDuration = Timer(1.0f, 0.005f);

    Rect CharacterZones;
    Rect CharacterThreshold;

    Sprite* BackgroundSprite;
    Sprite* BigAsteroidSprite;
    Sprite* SmallAsteroidSprite;
    Sprite* BulletSprite;
    Sprite* PowerShieldSprite;
    Sprite* GameOverSprite;
    Sprite* PauseSprite;
    Sprite* AutoShootingPowerUpSprite;
    Sprite* PowerShieldPowerUpSprite;
    Sprite* HomingBulletPowerUpSprite;

    Sprite* CurrentPowerUpSprite;

    const Actor* Character;
    Actor* Cursor;

    std::vector<Actor*> Asteroids;
    std::vector<Actor*> Zones[Grid * Grid];
    std::vector<Actor*> Bullets;
    std::vector<Actor*> AutoBullets;
    std::vector<Actor*> HomingBullets;
    std::vector<Actor*> PowerUps;

public:
    Asteroid(int ScreenWidth, int ScreenHeight, int MapWidth, int MapHeight, int EnemyNumber, int Ammo, float AbilityChance);

protected:
    void inRange(Actor* e);

    Rect getZones(int x, int y, int width, int height);

    Rect getZones(const Actor* e);

    void checkZoneCollision(int z);

    bool collidesWithZone(int x, int y, int width, int height, int z);

    bool collidesWithZone(const Actor* e, int z);

    bool newCollides(int x, int y, int width, int height);

    void spawnAbility(int x, int y);

    void sendBack(Actor* e);

    void split(Actor* enemy, Actor* bullet);

    void flyApart(Actor* e1, Actor* e2);

    void zoneActor(Actor* e);

    void zoneActors(std::vector<Actor*>& from);

    void zone();

    bool isAbility(const Actor* e);

    void collided(Actor* e1, Actor* e2);

    void characterCollided(Actor* entity);

    void autoShoot();

    void checkCharacterCollision();

    void checkCollisions();

    void moveActor(Actor* e);

    void moveActorBack(Actor* e);

    void moveActors(std::vector<Actor*>& from);

    void fillAsteroids();

    Actor* createBullet(const Actor* e);

    void addBullet();

    void addAutoBullet(const Actor* e);

    void addHomingBullet(const Actor* target);

    bool deleteActor(const Actor* e, std::vector<Actor*>& from);

    void updateTimers();

    void drawBackground();

    void drawActors(std::vector<Actor*>& from);

    void activatePowerUp();

    void checkPowerUpsEnded();

    void restart();

public:

    virtual void PreInit(int& width, int& height, bool& fullscreen) override;

    virtual bool Init() override;

    virtual void Close() override;

    virtual bool Tick() override;

    virtual void onMouseMove(int x, int y, int xrelative, int yrelative) override;

    virtual void onMouseButtonClick(FRMouseButton button, bool isReleased) override;

    virtual void onKeyPressed(FRKey k) override;

    virtual void onKeyReleased(FRKey k) override;

    virtual const char* GetTitle() override;
};