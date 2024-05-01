using Alpha_Danmaku_Rush_Demo.Src.Entities.Bullet;
using Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies.Decorator.Attack;
using Alpha_Danmaku_Rush_Demo.Src.Managers.Level;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Alpha_Danmaku_Rush_Demo.Src.Managers
{
    public class Regular : StrategyManager
    {
        SpriteBatch _spriteBatch;
        EnemyManager _enemyManager;
        //bullet control
        private List<EnemyBulletType> enemyBulletTypes;
        private Queue<Bullet> currBullets = new Queue<Bullet>();
        private Bullet testbullet;
        private Queue<Bullet> LoadedBullets = new Queue<Bullet>();
        TimeSpan AttackTimer = TimeSpan.Zero;



        public Regular(EnemyManager enemymanager, SpriteBatch sprite)
        {
            _enemyManager = enemymanager;
            _spriteBatch = sprite;

        }
        public Regular(SpriteBatch spriteBatch, EnemyManager enemyManager, List<EnemyBulletType> enemyBulletTypes, TimeSpan attackTimer)
        {
            _spriteBatch = spriteBatch;
            _enemyManager = enemyManager;
            this.enemyBulletTypes = enemyBulletTypes;
            //this.currBullets = currBullets;
            // this.testbullet = testbullet;
            //LoadedBullets = loadedBullets;
            AttackTimer = attackTimer;
        }
        public void update(EnemyManager enemyManager)
        {
            _enemyManager = enemyManager;
        }
        public void updateAttack(GameTime gameTime, int interval = 0)
        {

            if (interval != 0)
            {
                //Do control logic
            }
            else
            {
                AttackTimer += gameTime.ElapsedGameTime;
                if (AttackTimer.TotalSeconds > 2)//default attack interval
                {
                    AttackTimer = TimeSpan.Zero;
                    foreach (var enemy in _enemyManager.enemies)
                    {
                        Bullet bullet = enemy.bulletList.Dequeue();
                        LoadedBullets.Enqueue(bullet);
                    }

                }
            }
        }
        public void updateBullet()
        {
            if (LoadedBullets.Count > 0)
            {
                foreach (var item in LoadedBullets)
                {
                    item.Draw(_spriteBatch);
                    item.Update();
                }
            }
        }
        public void attackstrategy(GameTime gameTime)
        {

            updateAttack(gameTime);
            updateBullet();

        }

    }
    public class MidBoss : StrategyManager
    {
        SpriteBatch _spriteBatch;
        EnemyManager _enemyManager;
        Stack<MidBossBullet> bulletWave = new Stack<MidBossBullet>();
        Stack<MidBossBullet> subWave = new Stack<MidBossBullet>();
        (MidBossBullet, Stack<MidBossBullet>) pair;
        Stack<(MidBossBullet, Stack<MidBossBullet>)> pairstack = new Stack<(MidBossBullet, Stack<MidBossBullet>)>();
        ContentManager _content;
        EnemyBulletType Type;
        int AbilityCount;
        int f = 0;
        int r = 0;
        int prior = 10;
        TimeSpan firstTimer = TimeSpan.Zero;
        TimeSpan secondTimer = TimeSpan.Zero;
        int w, h;
        int yr = 10;
        Random rand = new Random();
        Boolean revert=false;
        float angle=(float) Math.PI;
        public MidBoss(EnemyManager enemymanager, SpriteBatch sprite, ContentManager _content, EnemyBulletType type)
        {
            _enemyManager = enemymanager;
            _spriteBatch = sprite;
            this._content = _content;
            this.Type = type;
            w = _enemyManager.getSize()[0];
            h = _enemyManager.getSize()[1];

        }
        public void attackstrategy(GameTime gameTime)
        {
            //need a ability control logic to make sure perform next ability after previous ability done
            Ability(gameTime);
            //AbilityCount++;
        }
        public void Ability(GameTime gameTime)
        {//Accourding to AbilityCount to perform ability
            switch (AbilityCount)
            {
                case 0://First ability: random attack
                    secondTimer += gameTime.ElapsedGameTime;
                    firstTimer += gameTime.ElapsedGameTime;
                    if (firstTimer >= TimeSpan.FromMilliseconds(500))
                    {
                        firstTimer = TimeSpan.Zero;
                        for (int i = 0; i < 10; i++)
                        {
                            // KiMeRuWaZa.Push(new Stack<MidBossBullet>());
                            float x = (float)rand.Next(-20, 20);
                            MidBossBullet bullet = (MidBossBullet)BulletFactory.CreateBullet(_content, new Vector2(w / 2, h / 2), new Vector2(x, 1), Type);
                            bulletWave.Push(bullet);


                        }

                    }



                    //Stack<MidBossBullet> bullets= new Stack<MidBossBullet>();
                    if (bulletWave.Count > 0)
                    {
                        foreach (var bullet in bulletWave)
                        {
                            bullet.Draw(_spriteBatch);
                            bullet.update1();
                        }

                    }



                    if (secondTimer >= TimeSpan.FromSeconds(5))
                    {
                        bulletWave.Clear();
                        AbilityCount++;
                        secondTimer = TimeSpan.Zero;
                    }
                    break;

                case 1:
                    firstTimer += gameTime.ElapsedGameTime;
                    secondTimer += gameTime.ElapsedGameTime;
                    if (secondTimer >= TimeSpan.FromMilliseconds(5000))
                    {
                        AbilityCount++;
                        bulletWave.Clear();
                        firstTimer = TimeSpan.Zero;
                        secondTimer = TimeSpan.Zero;
                    }

                    if (firstTimer >= TimeSpan.FromMilliseconds(450) && firstTimer <= TimeSpan.FromMilliseconds(550))
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            MidBossBullet bulletleft = (MidBossBullet)BulletFactory.CreateBullet(_content, new Vector2(0 + i * 20, h / 2), new Vector2(0, 1), Type);

                            MidBossBullet bulletright = (MidBossBullet)BulletFactory.CreateBullet(_content, new Vector2(w - i * 20, h / 2), new Vector2(0, 1), Type);
                            bulletWave.Push(bulletleft);
                            bulletWave.Push(bulletright);
                        }
                    }
                    if (firstTimer >= TimeSpan.FromMilliseconds(600) && firstTimer <= TimeSpan.FromMilliseconds(700))
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            MidBossBullet bulletleft = (MidBossBullet)BulletFactory.CreateBullet(_content, new Vector2(200 + i * 20, h / 2), new Vector2(0, 1), Type);

                            MidBossBullet bulletright = (MidBossBullet)BulletFactory.CreateBullet(_content, new Vector2(w - 200 - i * 20, h / 2), new Vector2(0, 1), Type);
                            bulletWave.Push(bulletleft);
                            bulletWave.Push(bulletright);
                        }
                    }
                    if (firstTimer >= TimeSpan.FromMilliseconds(750) && firstTimer <= TimeSpan.FromMilliseconds(850))
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            MidBossBullet bulletleft = (MidBossBullet)BulletFactory.CreateBullet(_content, new Vector2(400 + i * 20, h / 2), new Vector2(0, 1), Type);

                            MidBossBullet bulletright = (MidBossBullet)BulletFactory.CreateBullet(_content, new Vector2(w - 400 - i * 20, h / 2), new Vector2(0, 1), Type);
                            bulletWave.Push(bulletleft);
                            bulletWave.Push(bulletright);
                        }

                    }
                    if (firstTimer >= TimeSpan.FromMilliseconds(950) && firstTimer <= TimeSpan.FromMilliseconds(1000))
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            MidBossBullet bulletleft = (MidBossBullet)BulletFactory.CreateBullet(_content, new Vector2(600 + i * 20, h / 2), new Vector2(0, 1), Type);

                            MidBossBullet bulletright = (MidBossBullet)BulletFactory.CreateBullet(_content, new Vector2(w - 600 - i * 20, h / 2), new Vector2(0, 1), Type);
                            bulletWave.Push(bulletleft);
                            bulletWave.Push(bulletright);
                        }
                        firstTimer = TimeSpan.Zero;
                    }
                    if (bulletWave.Count > 0)
                    {
                        foreach (var bullet in bulletWave)
                        {
                            bullet.Draw(_spriteBatch);
                            bullet.update1(1.0f);
                        }

                    }
                    break;
                case 2://not completed
                    firstTimer += gameTime.ElapsedGameTime;
                    secondTimer += gameTime.ElapsedGameTime;
                    if (secondTimer >= TimeSpan.FromMilliseconds(5000))
                    {
                        AbilityCount++;
                        bulletWave.Clear();
                        subWave.Clear();
                        firstTimer = TimeSpan.Zero;
                        secondTimer = TimeSpan.Zero;
                        r = 30;
                    }
                    if (!revert) {
                        if (firstTimer >= TimeSpan.FromMilliseconds(150) && firstTimer <= TimeSpan.FromMilliseconds(250))
                        {

                            if (r <= 0)
                            {
                                r = w - 50;
                            }
                            else
                            {
                                r -= 50;
                            }
                            MidBossBullet bullet = (MidBossBullet)BulletFactory.CreateBullet(_content, new Vector2(r, h / 2), new Vector2(0, 1), Type);
                            bulletWave.Push(bullet);

                            firstTimer = TimeSpan.Zero;
                            if(r<=0)
                                revert=true;
                        }
                    }
                    if (revert)
                    {
                        if (firstTimer >= TimeSpan.FromMilliseconds(150) && firstTimer <= TimeSpan.FromMilliseconds(250))
                        {
                            if (r <= 0)
                                r = 50;

                            if (r >= w)
                            {
                                r =50;
                            }
                            else
                            {
                                r += 50;
                            }
                            MidBossBullet bullet = (MidBossBullet)BulletFactory.CreateBullet(_content, new Vector2(r, h / 2), new Vector2(0, 1), Type);
                            bulletWave.Push(bullet);

                            firstTimer = TimeSpan.Zero;
                            if (r >= w)
                            {
                                revert = false;
                            }
                        }
                        
                    }
                    
                    //foreach(var bullet in bulletWave)
                    //{
                    //    f += 30;

                    //    MidBossBullet bulletleft = (MidBossBullet)BulletFactory.CreateBullet(_content, new Vector2(bullet.Position.X-f, bullet.Position.Y), new Vector2(0, 1), Type);
                    //    MidBossBullet bulletright = (MidBossBullet)BulletFactory.CreateBullet(_content, new Vector2(bullet.Position.X+f, bullet.Position.Y), new Vector2(0,1), Type);
                    //    subWave.Push(bulletleft);
                    //    subWave.Push(bulletright);
                    //    pair = (bullet, subWave);
                    //    pairstack.Push(pair);

                    //}
                    //if (pairstack.Count > 0)
                    //{
                    //    foreach(var item in pairstack)
                    //    {
                    //        item.Item1.Draw(_spriteBatch);
                    //        item.Item1.update1();
                    //        foreach(var itemx in item.Item2)
                    //        {
                    //            itemx.Draw(_spriteBatch);
                    //            itemx.update1();
                    //        }
                    //    }
                    //}
                    if (bulletWave.Count > 0)
                    {
                        foreach (var bullet in bulletWave)
                        {
                            bullet.Draw(_spriteBatch);
                            bullet.update1();
                        }
                    }

                    //if (subWave.Count > 0)
                    //{
                    //    foreach(var bullet in subWave)
                    //    {
                    //        bullet.Draw(_spriteBatch);
                    //        bullet.update1(1.0f);
                    //    }
                    //}



                    break;
                case 3:
                    firstTimer += gameTime.ElapsedGameTime;
                    secondTimer += gameTime.ElapsedGameTime;
                    if (secondTimer >= TimeSpan.FromMilliseconds(5000))
                    {
                        AbilityCount++;
                        bulletWave.Clear();
                        subWave.Clear();
                        firstTimer = TimeSpan.Zero;
                        secondTimer = TimeSpan.Zero;
                        r = 10;
                    }
                    if (firstTimer >= TimeSpan.FromMilliseconds(100) && firstTimer <= TimeSpan.FromMilliseconds(150))
                    {
                        yr += 100;
                        r += 50;
                        MidBossBullet bullet = (MidBossBullet)BulletFactory.CreateBullet(_content, new Vector2(w / 2, h / 2), new Vector2(r, yr), Type);
                        MidBossBullet bullet1 = (MidBossBullet)BulletFactory.CreateBullet(_content, new Vector2(w / 2, h / 2), new Vector2(r + 50, yr + 200), Type);
                        MidBossBullet bullet2 = (MidBossBullet)BulletFactory.CreateBullet(_content, new Vector2(w / 2, h / 2), new Vector2(r + 100, yr + 400), Type);
                        bulletWave.Push(bullet);
                        bulletWave.Push(bullet1);
                        bulletWave.Push(bullet2);
                        MidBossBullet bullet3 = (MidBossBullet)BulletFactory.CreateBullet(_content, new Vector2(w / 2, h / 2), new Vector2(r + 150, yr + 600), Type);
                        MidBossBullet bullet4 = (MidBossBullet)BulletFactory.CreateBullet(_content, new Vector2(w / 2, h / 2), new Vector2(r + 200, yr + 800), Type);
                        MidBossBullet bullet5 = (MidBossBullet)BulletFactory.CreateBullet(_content, new Vector2(w / 2, h / 2), new Vector2(r + 250, yr + 1000), Type);
                        bulletWave.Push(bullet3);
                        bulletWave.Push(bullet4);
                        bulletWave.Push(bullet5);
                        firstTimer = TimeSpan.Zero;
                    }
                    if (bulletWave.Count > 0)
                    {
                        foreach (var bullet in bulletWave)
                        {
                            bullet.Draw(_spriteBatch);
                            bullet.update1();
                        }
                    }


                    break;

                default:
                    break;
            }

        }

    }
    public class FinalBoss : StrategyManager
    {
        public void attackstrategy(GameTime gameTime)
        {

        }
    }

}
