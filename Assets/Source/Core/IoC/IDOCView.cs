using strange.extensions.mediation.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Source.Core.IoC
{
    public interface IDOCView : IView
    {
        void Show();

        void Hide();
    }
}
