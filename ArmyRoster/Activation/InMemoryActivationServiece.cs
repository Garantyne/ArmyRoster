using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyRoster.Activation
{
    class InMemoryActivationServiece : IActivationService
    {
        private bool isActivated;
        private bool isTrialStarted;
        private int trialTimeMinut;
        private DateTime trialStartTime;
        public void Activate()
        {
            isActivated = true;
        }

        public bool IsActivated()
        {
            throw new NotImplementedException();
        }

        public bool IsTrialExpired()
        {
            if(!isTrialStarted)
            {
                return false;
            }
            double trialWorkTime = (DateTime.Now - trialStartTime).TotalMinutes;
            return trialWorkTime > trialTimeMinut;
        }

        public void StartTrial()
        {
            if(IsTrialExpired())
            {
                throw new InvalidOperationException();
            }
            if(!isTrialStarted)
            {
                isTrialStarted = true;
                trialStartTime = DateTime.Now;
            }
            isTrialStarted = true;
        }
    }
}
