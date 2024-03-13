using Alpha_Danmaku_Rush.Src.UI;

namespace Alpha_Danmaku_Rush.Src.Managers;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

public class SceneManager
{
    private GameState currentState;
    private int currentLevelIndex;
    private LevelManager levelManager; // 假设你有一个LevelManager类来管理关卡的加载和播放
    private MenuScreen menuScreen;

    public SceneManager(LevelManager levelManager, MenuScreen menuScreen)
    {
        this.levelManager = levelManager;
        currentState = GameState.MainMenu; // 游戏初始状态设为主菜单
        currentLevelIndex = 0; // 初始关卡索引
        this.menuScreen = menuScreen;
    }

    public void Update(GameTime gameTime)
    {
        KeyboardState state = Keyboard.GetState();

        // 根据当前状态更新逻辑
        switch (currentState)
        {
            case GameState.MainMenu:

                // 更新主菜单逻辑
                // 假设按下Enter键时开始游戏
                if (state.IsKeyDown(Keys.Enter))
                {
                    ChangeState(GameState.Playing);
                }

                break;
            case GameState.Settings:
                // 更新设置界面逻辑
                break;
            case GameState.Playing:
                // 更新当前关卡逻辑
                levelManager.Update(gameTime);
                break;
            case GameState.LevelComplete:
                // 关卡完成逻辑，可能包括切换到下一关
                NextLevel();
                break;
            case GameState.GameOver:
                // 游戏结束逻辑
                break;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // 根据当前状态绘制不同的场景
        // 示例：如果是Playing状态，绘制当前关卡
        if (currentState == GameState.Playing)
        {
            levelManager.Draw(spriteBatch);
        }
        // 对其他状态做类似处理
        else if (currentState == GameState.MainMenu)
        {
            menuScreen.Draw(spriteBatch);
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
        // 在这里添加任何状态切换时需要的逻辑，例如重置关卡索引
        if (newState == GameState.Playing)
        {
            // 可以在这里重置或初始化关卡相关的变量
            levelManager.NextLevel();
        }
    }

    private void NextLevel()
    {
        currentLevelIndex++;
        // 检查是否还有更多关卡，否则可能改变状态为游戏结束或循环关卡
        // 更新当前关卡或改变游戏状态
        currentState = GameState.Playing;
        levelManager.currentLevel = levelManager.levels[currentLevelIndex];
        levelManager.NextLevel();
    }
}

