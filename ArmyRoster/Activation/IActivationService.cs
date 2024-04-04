using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyRoster.Activation
{
    //Интерфейс сервиса активации приложения
    interface IActivationService
    {
        bool IsActivated(); //проверка активировалась или нет
        bool IsTrialExpired(); //проверка кончилась ли пробная версия

        void Activate(); //Выполнить активацию, в случае безошибочного срабатывания программа будет активирована
        void StartTrial();  //Запустить триальную версию

    }
}
