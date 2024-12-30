using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.ContentLoading
{
    public class ContentLoader
    {
        private static ContentLoader _instance;
        public static ContentLoader Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException("ContentLoader must be initialized before use. Call Initialize first.");
                }
                return _instance;
            }
        }

        public SpriteFont Font { get; private set; } 
        private Dictionary<string, Texture2D> _textures;
        private Dictionary<string, Song> _songs;
        private ContentManager _content;

        private ContentLoader()
        {
            _textures = new Dictionary<string, Texture2D>();
            _songs = new Dictionary<string, Song>();
        }

        public static void Initialize(ContentManager content)
        {
            if (_instance != null)
            {
                throw new InvalidOperationException("ContentLoader has already been initialized.");
            }

            _instance = new ContentLoader();
            _instance._content = content;
        }

        public void LoadSpriteFont(string contentName)
        {
            Font = _content.Load<SpriteFont>(contentName);
        }

        public Texture2D LoadTexture(string contentName)
        {
            if (!_textures.ContainsKey(contentName))
            {
                _textures.Add(contentName, _content.Load<Texture2D>(contentName));
            }
            return (_textures[contentName]);
        }

        public Song LoadSong(string contentName)
        {
            if (!_songs.ContainsKey(contentName))
            {
                _songs.Add(contentName, _content.Load<Song>(contentName));
            }
            return (_songs[contentName]);   
        }
    }
}
