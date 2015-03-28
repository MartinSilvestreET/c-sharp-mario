﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint4
{
    class PlayGameState :IGameState
    {
        Game1 game;
        int gameStateTransitionBuffer = 5;

        public PlayGameState(Game1 game)
        {
            game.keyboardController = new KeyboardController(game);
            this.game = game;
        }
        public void Update(GameTime gameTime)
        {
            if (gameStateTransitionBuffer > 0)
            {
                gameStateTransitionBuffer--;
            }
            else
            {
                game.keyboardController.Update();
                game.gamepadController.Update();
                game.level.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {     
            game.level.Draw(spriteBatch);   
        }
    }
}
