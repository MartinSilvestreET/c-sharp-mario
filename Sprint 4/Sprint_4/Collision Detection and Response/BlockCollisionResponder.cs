﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint4
{
    public class BlockCollisionResponder
    {
        ISpriteFactory factory = new SpriteFactory();
        Game1 game;


        public BlockCollisionResponder(Game1 game)
        {
            this.game = game;
        }
        
        public void MarioBlockCollide(Mario mario, Block block, List<Block> destroyedBlocks, List<Block> standingBlocks)
        {
            Rectangle marioRect = mario.state.getRectangle(new Vector2(mario.position.X, mario.position.Y));
            Rectangle blockRect = block.GetRectangle();
            Rectangle intersection = Rectangle.Intersect(marioRect, blockRect);

            if (intersection.Height > intersection.Width)
            {
                if (marioRect.Right > blockRect.Left && marioRect.Right < blockRect.Right)
                {
                    mario.position.X = mario.position.X - intersection.Width;
                }
                else
                {
                    mario.position.X = mario.position.X + intersection.Width;
                }
            }
            else
            {
                if (marioRect.Bottom > blockRect.Top && marioRect.Bottom < blockRect.Bottom)
                {
                    //This is the root of the vibration. The intersection height is too great, and is sending mario 
                     // into the air by a few pixels. Then falling state kicks in until he collides again, then bounces 
                       // back up. I've added 1 to his position to stop it, but this causes a tiny dip into the ground when he
                        //lands
                    mario.position.Y = mario.position.Y - intersection.Height +1;
                    standingBlocks.Add(block);
                }
                else
                {
                    mario.position.Y = mario.position.Y + intersection.Height;
                    block.Reaction();
                    mario.physState = new FallingState(game);
                    if (block.state.GetType().Equals(new BrickBlockState(game).GetType()) && mario.marioIsBig)
                    {
                        destroyedBlocks.Add(block);
                        game.level.soundManager.PlaySoundEffect(SoundManager.sfx.brick);
                    }
                }
            }
        }

        public void ItemBlockCollide(Item item, Block block)
        {
            Rectangle itemRect = item.GetRectangle();
            Rectangle blockRect = block.GetRectangle();
            Rectangle intersection = Rectangle.Intersect(itemRect, blockRect);
            if (intersection.Height > intersection.Width)
            {
                if (itemRect.Right > blockRect.Left && itemRect.Right < blockRect.Right)
                {
                    item.xpos = item.xpos - intersection.Width;
                }
                else
                {
                    item.xpos = item.xpos + intersection.Width;
                }
            }
            else
            {
                if (itemRect.Bottom > blockRect.Top && itemRect.Bottom < blockRect.Bottom)
                {
                    item.ypos = item.ypos - intersection.Height;
                }
                else
                {
                    item.ypos = item.ypos + intersection.Height;
                }
            }
        }

        public void EnemyBlockCollide(Enemy enemy, Block block)
        {
            Rectangle enemyRect = enemy.GetRectangle();
            Rectangle blockRect = block.GetRectangle();
            Rectangle intersection = Rectangle.Intersect(enemyRect, blockRect);
            if (intersection.Height > intersection.Width)
            {
                if (enemyRect.Right > blockRect.Left && enemyRect.Right < blockRect.Right)
                {
                    enemy.xpos = enemy.xpos - intersection.Width;
                }
                else
                {
                    enemy.xpos = enemy.xpos + intersection.Width;
                }
            }
            else
            {
                if (enemyRect.Bottom > blockRect.Top && enemyRect.Bottom < blockRect.Bottom)
                {
                    enemy.ypos = enemy.ypos - intersection.Height;
                }
                else
                {
                    enemy.ypos = enemy.ypos + intersection.Height;
                }
            }
        }
    }
}