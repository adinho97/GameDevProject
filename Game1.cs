using GameDevProject.Armament;
using GameDevProject.Characters;
using GameDevProject.Characters.Enemy;
using GameDevProject.Collisions;
using GameDevProject.ContentLoading;
using GameDevProject.GameState;
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
    private GameStateManager _gameStateManager;

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

    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        //initialize content loader
        ContentLoader.Initialize(Content);
        ContentLoader.Instance.LoadSpriteFont("fantasyFont");

        // Create the game state manager
        _gameStateManager = new GameStateManager(this);
        // Set the first state to be activated which is the start screen.
        _gameStateManager.SetState(new StartScreenState(_gameStateManager));

    }
    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        _gameStateManager.Update(gameTime);

        base.Update(gameTime);
    }



    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _gameStateManager.Draw(_spriteBatch);

        base.Draw(gameTime);
    }
}
