using GameDevProject.Characters;
using GameDevProject.Collisions;
using GameDevProject.Input;
using GameDevProject.Interfaces;
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

    private Texture2D HeroTexture;
    private Texture2D backgroundTexture;
    private Texture2D enemyTexture;
    private Texture2D projectileTexture; // add pro tex
    private Hero hero;
    private Enemy enemy;

    private List<IProjectile> projectiles = new List<IProjectile>(); //add  
    private BorderCollision borderCollision;

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
        var border = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        borderCollision = new BorderCollision(border);
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        enemyTexture = Content.Load<Texture2D>("menosGrande");
        HeroTexture = Content.Load<Texture2D>("tinyIchigo");
        backgroundTexture = Content.Load<Texture2D>("backgroundSand");
        projectileTexture = Content.Load<Texture2D>("SinglehollowCero");


        InitializeGameObject();

        // Define the screen or background boundary
        var screenBorder = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

        borderCollision = new BorderCollision(screenBorder);
        // TODO: use this.Content to load your game content here
    }

    private void InitializeGameObject()
    {
       
        hero = new Hero(HeroTexture, new KeyboardReader());
        enemy = new Enemy(enemyTexture, new Vector2(340, 200));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        hero.Update(gameTime, projectiles, projectileTexture); //add
        enemy.Update(gameTime);

        //add
        foreach (var projectile in projectiles)
        {
            projectile.Update(gameTime);
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
        
        _spriteBatch.Draw(backgroundTexture,
               new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height),
               Color.White);
        hero.Draw(_spriteBatch);
        enemy.Draw(_spriteBatch);

        // Draw projectiles
        foreach (var projectile in projectiles)
        {
            projectile.Draw(_spriteBatch);
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
