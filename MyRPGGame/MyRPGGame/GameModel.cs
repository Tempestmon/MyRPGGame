/*
 * Создать очерёдность хода, изменить методы
 * Создать вход
 * Создать ИИ
 * Добавить дополнительный урон, зависящий от статов
 */
namespace MyRPGGame
{
    public class GameModel
    {
        public GameModel()
        {
            var world = new World(11, 11) {IsFighting = false};
        }
    }
}