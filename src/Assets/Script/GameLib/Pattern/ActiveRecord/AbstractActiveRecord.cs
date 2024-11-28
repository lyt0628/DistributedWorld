using UnityEngine;

namespace QS.GameLib.Pattern
{

    /// <summary>
    /// 能不能使用组合的方式实现这个模版, 我不想破坏类结构
    /// </summary>
    public abstract class AbstractActiveRecord<T> : IActiveRecord<T>
    {
        bool newRecord = true;
        public bool NewRecord { get { return newRecord; } }

        protected T target;
        public T Unwrap() { return target; }
        public AbstractActiveRecord() {
        }

        public AbstractActiveRecord(T target) {
            this.target = target;
        }

        public bool Destroy()
        {
            if (BeforeDestroy())
            {
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
            if (BeforeSave())
            {   if(NewRecord)
                {
                    if (DoSave())
                    {
                        if (newRecord != false) newRecord = false;
                        AfterSave();
                        Debug.Log("Object saved and new record? " + newRecord);
                        return true;
                    }
                }
                Debug.LogError("You should not save a active record twice," +
                                 "It does  do nothing.");
                throw new System.InvalidOperationException();
            }
            return false;
        }

        protected abstract bool DoSave();

        public bool Update()
        {
            if (BeforeUpdate())
            {
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

        public void Wrap(T target)
        {
            this.target = target; 
        }
    }


}