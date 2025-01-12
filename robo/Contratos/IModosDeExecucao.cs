﻿using OpenQA.Selenium;
using robo.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robo.Contratos
{
    interface IModosDeExecucao
    {
        interface IModoComAlunos
        {
            void Executar(TOAluno aluno);
            void SelecionarMenu();
            void SetWebDriver(IWebDriver Driver);
        }

        interface IModoSemAlunos
        {
            void Executar();
            void SelecionarMenu();
            void SetWebDriver(IWebDriver Driver);
        }

        interface IFiesLegado
        {
            void TrocarCampus(string campus);
        }
    }
}
