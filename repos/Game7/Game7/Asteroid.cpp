#include <cmath>
#include <queue>
#include "Asteroid.h"
#include "Actor.h"

const char* Asteroid ::Title = "Asteroid";

struct DistanceCompare {
    const Actor* Character;

    DistanceCompare(const Actor* Character) : Character(Character) {}

    bool operator()(Actor* lhs, Actor* rhs) {
        return Character->distance(*lhs) > Character->distance(*rhs);
    }
};

Asteroid::Asteroid(int ScreenWidth, int ScreenHeight, int MapWidth, int MapHeight, int AsteroidNumber, int Ammo, float AbilityChance)
    : ScreenWidth(ScreenWidth), ScreenHeight(ScreenHeight), MapWidth(MapWidth),
    MapHeight(MapHeight), AsteroidNumber(AsteroidNumber), Ammo(Ammo), AbilityChance(AbilityChance)
{}

void Asteroid::inRange(Actor* e) {
    if (e->x() < -(e->width() + DeltaWidth))
        e->x() += e->width() + MapWidth;
    else if (e->x() > ScreenWidth + DeltaWidth)
        e->x() -= e->width() + MapWidth;
    if (e->y() < -(e->height() + DeltaHeight))
        e->y() += e->height() + MapHeight;
    else if (e->y() > ScreenHeight + DeltaHeight)
        e->y() -= e->height() + MapHeight;
}

Rect Asteroid::getZones(int x, int y, int width, int height) {
    int x1 = x <= -DeltaWidth || x + width >= ScreenWidth + DeltaWidth ? 0 : (int)(x + DeltaWidth) / GridWidth;
    int x2 = x <= -DeltaWidth || x + width >= ScreenWidth + DeltaWidth ? 0 : (int)(x + width + DeltaWidth) / GridWidth;
    int y1 = y <= -DeltaHeight || y + height >= ScreenHeight + DeltaHeight ? 0 : (int)(y + DeltaHeight) / GridHeight;
    int y2 = y <= -DeltaHeight || y + height >= ScreenHeight + DeltaHeight ? 0 : (int)(y + height + DeltaHeight) / GridHeight;
    int x1y1 = y1 * Grid + x1;
    int x2y1 = y1 * Grid + x2;
    int x1y2 = y2 * Grid + x1;
    int x2y2 = y2 * Grid + y2;
    return Rect{ x1y1, x2y1, x1y2, x2y2 };
}

Rect Asteroid::getZones(const Actor* e) {
    return getZones(e->x(), e->y(), e->width(), e->height());
}

void Asteroid::checkZoneCollision(int z) {
    for (int i = 0; i < Zones[z].size() == 0 ? 0 : Zones[z].size() - 1; i++)
        for (int j = i + 1; j < Zones[z].size(); j++)
            if (Zones[z][i]->intersect(*Zones[z][j]))
                collided(Zones[z][i], Zones[z][j]);
}

bool Asteroid::collidesWithZone(int x, int y, int width, int height, int z) {
    for (Actor* enemy : Zones[z]) {
        if (enemy->intersect(x, y, width, height))
            return true;
    }
    return false;
}

bool Asteroid::collidesWithZone(const Actor* e, int z) {
    return collidesWithZone(e->x(), e->y(), e->width(), e->height(), z);
}

bool Asteroid::newCollides(int x, int y, int width, int height) {
    Rect r = getZones(x, y, width, height);
    for (int j = r.x1y1; j <= r.x1y2; j += Grid)
        for (int i = j; i <= j + (r.x2y1 - r.x1y1); i++)
            if (collidesWithZone(x, y, width, height, i))
                return true;
    return false;
}

void Asteroid::spawnAbility(int x, int y) {
    Sprite* powerUpSprite;
    switch (rand() % 3) {
    case 0:
        powerUpSprite = AutoShootingPowerUpSprite;
        break;
    case 1:
        powerUpSprite = PowerShieldPowerUpSprite;
        break;
    case 2:
        powerUpSprite = HomingBulletPowerUpSprite;;
        break;
    default:
        return;
    }
    PowerUps.push_back(new Actor(powerUpSprite, 0.0f, 0.0f, x, y));
}

void Asteroid::sendBack(Actor* e) {
    bool isBig = rand() % 2;
    int x, y, width, height, entityWidth, entityHeight, r;
    getSpriteSize(isBig ? BigAsteroidSprite : SmallAsteroidSprite, entityWidth, entityHeight);
    do {
        if (rand() % 2) {
            x = rand() % (MapWidth * 2) - MapWidth;
            r = DeltaHeight == 0 ? 0 : rand() % DeltaHeight;
            y = rand() % 2 ? -(r + entityHeight) : ScreenHeight + r;
        }
        else {
            r = DeltaWidth == 0 ? 0 : rand() % DeltaWidth;
            x = rand() % 2 ? -(r + entityWidth) : ScreenWidth + r;
            y = rand() % (MapHeight * 2) - MapHeight;
        }
        getSpriteSize(isBig ? BigAsteroidSprite : SmallAsteroidSprite, width, height);
    } while (Character->intersect(x - Threshold, y - Threshold,
        width + Threshold * 2, height + Threshold * 2) || newCollides(x, y, width, height));
    float constSpeedX = (float)(rand() % (int)(Actor::maxSpeed * 20.0f)) / 100.0f;
    float constSpeedY = (float)(rand() % (int)(Actor::maxSpeed * 20.0f)) / 100.0f;
    constSpeedX = rand() % 2 ? constSpeedX : -constSpeedX;
    constSpeedY = rand() % 2 ? constSpeedY : -constSpeedY;
    if (rand() % 101 <= (int)(AbilityChance * 100))
        spawnAbility(e->x(), e->y());
    e->x() = x;
    e->y() = y;
    e->constSpeedX() = constSpeedX;
    e->constSpeedY() = constSpeedY;
    e->setSprite(isBig ? BigAsteroidSprite : SmallAsteroidSprite);
}

void Asteroid::split(Actor* enemy, Actor* bullet) {
    enemy->x() -= enemy->width() / 2;
    enemy->y() -= enemy->height() / 2;
    enemy->setSprite(SmallAsteroidSprite);
    bullet->constSpeedX() /= Actor::maxSpeed / 2;
    bullet->constSpeedY() /= Actor::maxSpeed / 2;
    enemy->constSpeedX() = (enemy->constSpeedX() + bullet->constSpeedX()) / 2;
    enemy->constSpeedY() = (enemy->constSpeedY() + bullet->constSpeedY()) / 2;
    Asteroids.push_back(
        new Actor(SmallAsteroidSprite, -enemy->constSpeedX(), -enemy->constSpeedY(),
            enemy->x() + enemy->width(), enemy->y() + enemy->height()));
}

void Asteroid::flyApart(Actor* e1, Actor* e2) {
    float speedX = e1->constSpeedX();
    float speedY = e1->constSpeedY();
    e1->constSpeedX() = e2->constSpeedX();
    e1->constSpeedY() = e2->constSpeedY();
    e2->constSpeedX() = speedX;
    e2->constSpeedY() = speedY;
    while (e1->intersect(*e2)) {
        e1->x() -= e1->constSpeedX();
        e2->x() -= e2->constSpeedX();
        e1->y() -= e1->constSpeedY();
        e2->y() -= e2->constSpeedY();
    }
}

void Asteroid::zoneActor(Actor* e) {
    Rect r = getZones(e);
    for (int j = r.x1y1; j <= r.x1y2; j += Grid)
        for (int i = j; i <= j + (r.x2y1 - r.x1y1); i++)
            Zones[i].push_back(e);
}

void Asteroid::zoneActors(std::vector<Actor*>& from) {
    for (Actor* e : from)
        zoneActor(e);
}

void Asteroid::zone() {
    for (int i = 0; i < Grid * Grid; i++)
        Zones[i].clear();

    // зона усіх астероїдів
    zoneActors(Asteroids);

    // зона усіх вистрілів
    zoneActors(Bullets);
    zoneActors(AutoBullets);
    zoneActors(HomingBullets);

    zoneActors(PowerUps);
}

bool Asteroid::isAbility(const Actor* e) {
    return e->getSprite() == AutoShootingPowerUpSprite || e->getSprite() == PowerShieldPowerUpSprite || e->getSprite() == HomingBulletPowerUpSprite;
}

void swap(Actor*& e1, Actor*& e2) {
    Actor* temp = e1;
    e1 = e2;
    e2 = temp;
}

void Asteroid::collided(Actor* e1, Actor* e2) {
    if ((e1->getSprite() == BulletSprite && e2->getSprite() == BulletSprite) || (isAbility(e1) || isAbility(e2)))
        return;

    if (e2->getSprite() == BulletSprite)
        swap(e1, e2);

    if (e1->getSprite() == BulletSprite) {
        if (e2->getSprite() == SmallAsteroidSprite)
            sendBack(e2);
        else
            split(e2, e1);

        if (!deleteActor(e1, Bullets))
            if (!deleteActor(e1, AutoBullets))
                deleteActor(e1, HomingBullets);
        return;
    }

    flyApart(e1, e2);
}

void Asteroid::characterCollided(Actor* entity) {
    if (entity->getSprite() == BulletSprite)
        return;

    if (isAbility(entity)) {
        CurrentPowerUpSprite = entity->getSprite();
        bHasPowerUp = true;
        deleteActor(entity, PowerUps);
        return;
    }

    if (bPowerShield) {
        entity->x() += entity->constSpeedX();
        entity->y() += entity->constSpeedY();
        return;
    }

    bPaused = true;
    bGameOver = true;
}

void Asteroid::autoShoot() {
    static std::priority_queue<Actor*, std::vector<Actor*>, DistanceCompare> distances((DistanceCompare(Character)));
    distances = std::priority_queue<Actor*, std::vector<Actor*>, DistanceCompare>(DistanceCompare(Character));
    if (AutoShootingDelay.ended()) {
        for (int j = CharacterThreshold.x1y1; j <= CharacterThreshold.x1y2; j += Grid) {
            for (int i = j; i <= j + (CharacterThreshold.x2y1 - CharacterThreshold.x1y1); i++) {
                for (Actor* enemy : Zones[i]) {
                    if (enemy == Character || enemy->getSprite() == BulletSprite)
                        continue;
                    if (enemy->intersect(Character->x() - Threshold, Character->y() - Threshold, Character->width() + Threshold * 2, Character->height() + Threshold * 2)) {
                        distances.push(enemy);
                    }
                }
            }
        }
        if (!distances.empty()) {
            Actor* t = distances.top();
            addAutoBullet(t);
            AutoShootingDelay.begin();
            distances.pop();
        }
    }
}

void Asteroid::checkCharacterCollision() {
    for (int j = CharacterZones.x1y1; j <= CharacterZones.x1y2; j += Grid)
        for (int i = j; i <= j + (CharacterZones.x2y1 - CharacterZones.x1y1); i++)
            for (Actor* enemy : Zones[i])
                if (Character->intersect(*enemy))
                    characterCollided(enemy);
}

void Asteroid::checkCollisions() {
    zone();

    for (int z = 0; z < Grid * Grid; z++)
        checkZoneCollision(z);

    checkCharacterCollision();

    if (bAutoShooting)
        autoShoot();
}

void Asteroid::moveActor(Actor* e) {
    e->move();
    inRange(e);
}

void Asteroid::moveActorBack(Actor* e) {
    e->moveBack();
    inRange(e);
}

void Asteroid::moveActors(std::vector<Actor*>& from) {
    for (Actor* e : from)
        moveActorBack(e);
}

void Asteroid::fillAsteroids() {
    Asteroids.clear();
    for (int i = 0; i < AsteroidNumber; i++) {
        bool isBig = rand() % 2;
        int x, y, width, height;
        do {
            x = rand() % MapWidth - DeltaWidth * 2;
            y = rand() % MapHeight - DeltaHeight * 2;
            getSpriteSize(isBig ? BigAsteroidSprite : SmallAsteroidSprite, width, height);
        } while (Character->intersect(x - Threshold, y - Threshold,
            width + Threshold * 2, height + Threshold * 2) || newCollides(x, y, width, height));
        float constSpeedX = (float)(rand() % (int)(Actor::maxSpeed * 20.0f)) / 100.0f;
        float constSpeedY = (float)(rand() % (int)(Actor::maxSpeed * 20.0f)) / 100.0f;
        constSpeedX = rand() % 2 ? constSpeedX : -constSpeedX;
        constSpeedY = rand() % 2 ? constSpeedY : -constSpeedY;
        Asteroids.push_back(new Actor(isBig ? BigAsteroidSprite
            : SmallAsteroidSprite, constSpeedX, constSpeedY, x, y));
    }
}

Actor* Asteroid::createBullet(const Actor* e) {
    static float x = Character->x() + (Character->width() - BulletSpriteWidth) / 2;
    static float y = Character->y() + (Character->height() - BulletSpriteHeight) / 2;
    float constSpeedX = e->x() - x;
    float constSpeedY = e->y() - y;
    float len = sqrtf(constSpeedX * constSpeedX + constSpeedY * constSpeedY);
    constSpeedX *= Actor::maxSpeed / -len;
    constSpeedY *= Actor::maxSpeed / -len;
    return new Actor(BulletSprite, constSpeedX, constSpeedY, x, y);
}

void Asteroid::addBullet() {
    if (Bullets.size() >= Ammo)
        Bullets.erase(Bullets.begin());
    Bullets.push_back(createBullet(Cursor));
}

void Asteroid::addAutoBullet(const Actor* e) {
    if (AutoBullets.size() >= AutoAmmo)
        AutoBullets.erase(AutoBullets.begin());
    AutoBullets.push_back(createBullet(e));
}

void Asteroid::addHomingBullet(const Actor* target) {
    Actor* bullet = createBullet(Cursor);
    if (target != nullptr) {
        bullet->constSpeedX() += target->constSpeedX();
        bullet->constSpeedY() += target->constSpeedY();
    }
    HomingBullets.push_back(bullet);
}

bool Asteroid::deleteActor(const Actor* e, std::vector<Actor*>& from) {
    for (int i = 0; i < from.size(); i++)
        if (from[i] == e) {
            from.erase(from.begin() + i);
            return true;
        }
    return false;
}

void Asteroid::updateTimers() {
    ShootingDelay.tick();
    AutoShootingDelay.tick();
    AutoShootingDuration.tick();
    PowerShieldDuration.tick();
}

void Asteroid::drawBackground() {
    static float deltaX = 0.0f;
    static float deltaY = 0.0f;
    deltaX += Actor::speedX;
    deltaY += Actor::speedY;

    if (deltaX > BackgroundSpriteWidth)
        deltaX -= BackgroundSpriteWidth;
    else if (deltaX < -BackgroundSpriteWidth)
        deltaX += BackgroundSpriteWidth;
    if (deltaY > BackgroundSpriteHeight)
        deltaY -= BackgroundSpriteHeight;
    else if (deltaY < -BackgroundSpriteHeight)
        deltaY += BackgroundSpriteHeight;

    for (int j = -1; j < ScreenHeight / BackgroundSpriteHeight + 2; j++) {
        for (int i = -1; i < ScreenWidth / BackgroundSpriteWidth + 2; i++) {
            drawSprite(BackgroundSprite,
                BackgroundSpriteWidth * i - deltaX - 3 * (i + 1),
                BackgroundSpriteHeight * j - deltaY - 3 * (j + 1));
        }
    }
}

void Asteroid::drawActors(std::vector<Actor*>& from) {
    for (Actor* e : from)
        e->draw();
}

void Asteroid::activatePowerUp() {
    if (CurrentPowerUpSprite == AutoShootingPowerUpSprite) {
        bAutoShooting = true;
        bHasPowerUp = false;
        AutoShootingDuration.begin();
        return;
    }
    if (CurrentPowerUpSprite == PowerShieldPowerUpSprite) {
        bPowerShield = true;
        bHasPowerUp = false;
        PowerShieldDuration.begin();
        return;
    }
    if (CurrentPowerUpSprite == HomingBulletPowerUpSprite) {
        bHasPowerUp = false;
        int zone = getZones(Cursor).x1y1;
        Actor* target = nullptr;
        for (Actor* enemy : Zones[zone]) {
            if (enemy->getSprite() != SmallAsteroidSprite && enemy->getSprite() != BigAsteroidSprite)
                continue;
            if (Cursor->intersect(*enemy))
                target = enemy;
        }
        addHomingBullet(target);
    }
}

void Asteroid::checkPowerUpsEnded() {
    if (bAutoShooting && AutoShootingDuration.ended())
        bAutoShooting = false;
    if (bPowerShield && PowerShieldDuration.ended())
        bPowerShield = false;
}

void Asteroid::restart() {
    fillAsteroids();
    Bullets.clear();
    AutoBullets.clear();
    HomingBullets.clear();
    PowerUps.clear();
    ShootingDelay.reset();
    AutoShootingDelay.reset();
    Actor::speedX = 0.0f;
    Actor::speedY = 0.0f;
    bGameOver = false;
    bPaused = false;
    bHasPowerUp = false;
}

void Asteroid::PreInit(int& width, int& height, bool& fullscreen) {
    width = ScreenWidth;
    height = ScreenHeight;
    fullscreen = bFullscreen;
}

bool Asteroid::Init() {
    if (bFullscreen)
        getScreenSize(ScreenWidth, ScreenHeight);

    if (MapWidth < ScreenWidth || MapHeight < ScreenHeight)
        return false;

    showCursor(false);

    BackgroundSprite = createSprite("C:/Users/Sophie/source/repos/Game7/data/fon.jpg");

    Sprite* characterSprite = createSprite("C:/Users/Sophie/source/repos/Game7/data/spaceship.png");
    int characterWidth, characterHeight;
    getSpriteSize(characterSprite, characterWidth, characterHeight);
    Character = new Actor(characterSprite, 0.0f, 0.0f,
        (ScreenWidth - characterWidth) / 2, (ScreenHeight - characterHeight) / 2);
    CharacterZones = getZones(Character);
    CharacterThreshold = getZones(Character->x() - Threshold, Character->y() - Threshold, Character->width() + Threshold * 2, Character->height() + Threshold * 2);

    PowerShieldSprite = createSprite("C:/Users/Sophie/source/repos/Game7/data/power_shield.png");

    Cursor = new Actor(createSprite("C:/Users/Sophie/source/repos/Game7/data/circle.tga"));

    GameOverSprite = createSprite("C:/Users/Sophie/source/repos/Game7/data/gameover.png");
    PauseSprite = createSprite("C:/Users/Sophie/source/repos/Game7/data/pause.png");

    BigAsteroidSprite = createSprite("C:/Users/Sophie/source/repos/Game7/data/big_asteroid.png");
    SmallAsteroidSprite = createSprite("C:/Users/Sophie/source/repos/Game7/data/small_asteroid.png");

    BulletSprite = createSprite("C:/Users/Sophie/source/repos/Game7/data/bullet.png");
    getSpriteSize(BulletSprite, BulletSpriteWidth, BulletSpriteHeight);

    getSpriteSize(BackgroundSprite, BackgroundSpriteWidth, BackgroundSpriteHeight);

    AutoShootingPowerUpSprite = createSprite("C:/Users/Sophie/source/repos/Game7/data/reticle.png");
    PowerShieldPowerUpSprite = createSprite("C:/Users/Sophie/source/repos/Game7/data/enemy.png");
    HomingBulletPowerUpSprite = createSprite("C:/Users/Sophie/source/repos/Game7/data/reticle_2.png");

    if (!(BackgroundSprite && Character->getSprite() && PowerShieldSprite && Cursor->getSprite() && BigAsteroidSprite && SmallAsteroidSprite && BulletSprite && AutoShootingPowerUpSprite && PowerShieldPowerUpSprite && HomingBulletPowerUpSprite))
        return false;

    restart();

    return true;
}

void Asteroid::Close() {

    // Видаляємо усіх Actors
    destroySprite(BackgroundSprite);
    destroySprite(GameOverSprite);
    destroySprite(BigAsteroidSprite);
    destroySprite(SmallAsteroidSprite);
    destroySprite(Character->getSprite());
    destroySprite(Cursor->getSprite());

    delete Character;
    delete Cursor;

    Asteroids.clear();
    Bullets.clear();
    HomingBullets.clear();
}

bool Asteroid::Tick() {
    if (bPaused) {
        static int width, height;
        getSpriteSize(bGameOver ? drawBackground(), GameOverSprite : nullptr, width, height);
        drawSprite(bGameOver ? GameOverSprite : nullptr,
            (ScreenWidth - width) / 2, (ScreenHeight - height) / 2);
    }
    else {
        drawBackground();

        //появлення акторів на екран
        drawActors(Bullets);
        drawActors(AutoBullets);
        drawActors(HomingBullets);
        Character->draw();
        if (bPowerShield)
            drawSprite(PowerShieldSprite, Character->x(), Character->y());
        drawActors(PowerUps);
        drawActors(Asteroids);

        // Рух акторів
        moveActors(Asteroids);
        moveActors(Bullets);
        moveActors(AutoBullets);
        moveActors(HomingBullets);
        moveActors(PowerUps);

        if (bHasPowerUp)
            drawSprite(CurrentPowerUpSprite, 0, 0);

        checkCollisions();

        checkPowerUpsEnded();

        Actor::updateSpeed();

        updateTimers();
    }

    Cursor->drawCentered();

    return false;
}

void Asteroid::onMouseMove(int x, int y, int xrelative, int yrelative) {
    Cursor->x() = x;
    Cursor->y() = y;
}

void Asteroid::onMouseButtonClick(FRMouseButton button, bool isReleased) {
    if (isReleased)
        return;

    if (bGameOver) {
        restart();
        return;
    }

    if (bPaused) {
        bPaused = false;
        return;
    }

    switch (button) {
    case FRMouseButton::LEFT:
        if (ShootingDelay.ended()) {
            addBullet();
            ShootingDelay.begin();
        }
        return;

    case FRMouseButton::MIDDLE:
        bPaused = true;
        return;

    case FRMouseButton::RIGHT:
        if (bHasPowerUp)
            activatePowerUp();
        return;

    default:
        return;
    }
}

void Asteroid::onKeyPressed(FRKey k) {
    Actor::shootingManager[k] = true;
}

void Asteroid::onKeyReleased(FRKey k) {
    Actor::shootingManager[k] = false;
}

const char* Asteroid::GetTitle() {
    return Title;
}