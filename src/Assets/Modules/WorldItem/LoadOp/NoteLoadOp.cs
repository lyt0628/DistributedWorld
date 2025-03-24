

using QS.Common;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace QS.WorldItem
{
    class NoteLoadOp : AsyncOpBase<INote>
    {
        readonly DefaultNote note;

        public NoteLoadOp(DefaultNote note)
        {
            this.note = note;
        }

        protected override async void Execute()
        {
            var loadImg = Addressables.LoadAssetAsync<Sprite>(note.imageAddress);
            await loadImg.Task;
            note.image = loadImg.Result;
            Complete(note);
        }
    }
}