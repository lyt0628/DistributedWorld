


namespace GameLib.Pattern
{
    
   public abstract class AbstractActiveRecord : IActiveRecord
   {
            bool newRecord = true;
            public bool NewRecord { get { return newRecord; } }
            public abstract bool Persisted { get; protected set; }

            public bool Destroy()
            {
                if (BeforeDestroy()){
                    if (DoDestroy())
                    {
                        AfterDestroy();
                        return true;
                    }
                }
                return false;
            }


            protected abstract bool DoDestroy();

            public bool Save()
            {
                if (BeforeSave()) {
                    if (DoSave())
                    {
                        if(newRecord != false) newRecord = false;
                        AfterSave();
                        return true;
                    }
                }
                return false;
            }

            protected abstract bool DoSave();

            public bool Update()
            {
                if (BeforeUpdate()) {
                    DoUpdate();
                    AfterUpdate();
                    return true;
                }
                return false;
            }

            protected abstract void DoUpdate();

            protected virtual void AfterDestroy()
            {
            }

            protected virtual void AfterSave()
            {
            }

            protected virtual void AfterUpdate()
            {
            }

            protected virtual bool BeforeDestroy()
            {
                return true;
            }

            protected virtual bool BeforeSave()
            {
                return true;
            }

            protected virtual bool BeforeUpdate()
            {
                return true;
            }
                 
   }


}