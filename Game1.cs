using GameDevProject.Armament;
using GameDevProject.Characters;
using GameDevProject.Characters.Enemy;
using GameDevProject.Collisions;
using GameDevProject.Input;
using GameDevProject.Interfaces;
using GameDevProject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GameDevProject;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D debugTexture;

    private Texture2D HeroTexture;
    private Texture2D backgroundTexture;
    private Texture2D enemyTexture;
    private Texture2D projectileTexture; // add pro tex
    private Hero hero;
    private EnemyManager enemyManager;
    private SpriteFont font;
    private int score = 0;
    private ProjectileManager projectileManager;
    private KeyboardReader keyboardReader;

    private List<IProjectile> projectiles = new List<IProjectile>(); //add  
    private BorderCollision borderCollision;
    private List<ICollidable> collidables;
    private UIManager uiManager;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        
        base.Initialize();
        
        _graphics.PreferredBackBufferWidth = 1620;  // Set your desired width
        _graphics.PreferredBackBufferHeight = 860; // Set your desired height
        _graphics.ApplyChanges();
    
        var border = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        borderCollision = new BorderCollision(border);
        collidables = new List<ICollidable>();
        


    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        debugTexture = new Texture2D(GraphicsDevice, 1, 1); 
        debugTexture.SetData(new[] { Color.White });
        enemyTexture = Content.Load<Texture2D>("snakeSprite");
        HeroTexture = Content.Load<Texture2D>("tinyIchigo");
        backgroundTexture = Content.Load<Texture2D>("backgroundSand");
        projectileTexture = Content.Load<Texture2D>("SinglehollowCero");
        projectileManager = new ProjectileManager();
        //scoreFont = Content.Load<SpriteFont>("ScoreFont");
        uiManager = new UIManager();
        uiManager.LoadContent(Content);

        InitializeGameObject();

        // Define the screen or background boundary
        var screenBorder = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

        borderCollision = new BorderCollision(screenBorder);
        // TODO: use this.Content to load your game content here
        font = Content.Load<SpriteFont>("fantasyFont");
    }

    private void InitializeGameObject()
    {

        hero = new Hero(HeroTexture, projectileManager, projectileTexture, debugTexture, new KeyboardReader());
        enemyManager = new EnemyManager(enemyTexture, collidables);
    }




    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (uiManager.IsStartScreenActive()) // Check if the start screen is active
        {
            uiManager.Update(gameTime); // Update the start screen logic (e.g., detect spacebar press)
            return; // Don't update the game objects until space is pressed
        }

        // TODO: Add your update logic here
        hero.Update(gameTime); //add
        
        CollisionManager.HandleCollisions(hero, collidables);

        //EnemyManager handle enemies
        enemyManager.Update(gameTime, hero.Position);

        //add
        foreach (var projectile in projectiles)
        {
            projectile.Update(gameTime);
        }
        //detect bullet collision w enemies (move later to like a gameworld class)
        var enemies = enemyManager.GetEnemies(); // Get the current enemies from the manager
        for (int i = enemies.Count - 1; i >= 0; i--) 
        {
            var enemy = enemies[i];
            Rectangle enemyHitbox = enemy.GetBorder();

            for (int j = projectiles.Count - 1; j >= 0; j--) 
            {
                var projectile = projectiles[j];
                Rectangle projectileHitbox = projectile.GetBorder();

                if (enemyHitbox.Intersects(projectileHitbox))
                {
                    //enemy is hit
                    enemies.RemoveAt(i);
                    score += 10;
                    projectiles.RemoveAt(j);
                    break;
                }
            }
            collidables.Add(enemy); // allows hero to collide with enemy
        }

        //add
        projectiles.RemoveAll(p => !p.IsActive);
    
        // Enforce collision constraints
        borderCollision.Constrain(hero);
        base.Update(gameTime);
    }



    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        // Draw the start screen if it's active
        uiManager.Draw(_spriteBatch);
        if (!uiManager.IsStartScreenActive())
        {
            _spriteBatch.Draw(backgroundTexture,
               new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height),
               Color.White);
            hero.Draw(_spriteBatch);
            _spriteBatch.DrawString(font, "SCORE: " + score, new Vector2(20,20), Color.Black);

            enemyManager.Draw(_spriteBatch, debugTexture);

            // Draw projectiles
            foreach (var projectile in projectiles)
            {
                projectile.Draw(_spriteBatch);
            }
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
