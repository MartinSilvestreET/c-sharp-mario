﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Sprint4
{
    public class Fireball
    {
        IAnimatedSprite sprite;
        ISpriteFactory factory;
        public bool left = true;
        public Vector2 position;
        public int fireballLifespan = 100;
        int moveRate = 2;

        public Fireball(Vector2 location, bool left)
        {
            factory = new SpriteFactory();
            sprite = factory.build(SpriteFactory.sprites.fireball);
            position = location;
            this.left = left;
            SoundManager.fireball.Play();
        }

        public void GoLeft()
        {
            position.X -= moveRate;
            left = true;            
        }
        public void GoRight()
        {
            position.X += moveRate;
            left = false;            
        }
        public void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
            if (fireballLifespan != 0)
            {
                fireballLifespan--;
                if (left)
                {
                    GoLeft();
                }
                else
                {
                    GoRight();
                }
            }
            position.Y = position.Y + 1;     
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, position);        
        }
        public Rectangle GetBoundingBox()
        {
            return sprite.GetBoundingBox(position);
        }
    }
}