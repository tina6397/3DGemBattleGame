using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaceGame
{
    class Armoury
    {
        public bool gunChanged { get; set; }
        public Gun activeGun { get; set; }

        public List<Gun> collectedGuns { get; set; }

        /// <summary>
        /// A constructor to initialize the list of guns
        /// </summary>
        public Armoury()
        {
            collectedGuns = new List<Gun>();
        }


        public void ChangeGun (Gun gun){
            activeGun = gun ;
            gunChanged = true;
        }

        public void SwapGun (int index){
            if ((collectedGuns!=null)&& (activeGun!=null)&&(index < collectedGuns.Count)){
                ChangeGun(collectedGuns[index]);
            }
        }


        public void AddGun(Gun gun)
        {
            bool add = true;
            foreach (Gun g in collectedGuns)
            {
                if (add && g.GetType() == gun.GetType())
                {
                    g.ReloadAmmo();
                    ChangeGun(g);
                    add = false;
                }
            }

            if (add)
            {
                ChangeGun(gun);
                collectedGuns.Add(gun);
            }
            else
            {
                gun.Dispose();
            }
        }



        public void Dispose()
        {

            if (activeGun != null)
            {
                activeGun.Dispose();
            }

            foreach (Gun g in collectedGuns)
            {
                g.Dispose();

            }

        }
    }
}
