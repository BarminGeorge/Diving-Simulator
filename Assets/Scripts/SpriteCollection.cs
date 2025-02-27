using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCollection : MonoBehaviour
{
    public Sprite FirstClassStand;
    public Sprite ThirdClassStand;
    public Sprite FrontArmStand;
    public Sprite BackArmStand;
    public Sprite TuckFront;
    public Sprite PikeFront;
    public Sprite EntranceFront;
    public Sprite TuckBack;
    public Sprite PikeBack;
    public Sprite EntranceBack;
    public Dictionary<DiveType, Sprite> StandSprites;
    public Dictionary<DiveType, Dictionary<DivePosition, Sprite>> PositionSprites;
    
    public Sprite[] AllMarksSprites;

    private void Start()
    {
        InitializeStandSprites();
        InitializePositionSprites();
    }

    private void InitializeStandSprites()
    {
        StandSprites = new Dictionary<DiveType, Sprite>
        {
            { DiveType.FirstClass, FirstClassStand },
            { DiveType.SecondClass, UnfoldSprite(ThirdClassStand) },
            { DiveType.ThirdClass, ThirdClassStand },
            { DiveType.FourthClass, UnfoldSprite(FirstClassStand) },
            { DiveType.SixthFrontClass, FrontArmStand },
            { DiveType.SixthBackClass, BackArmStand },
            { DiveType.SixthThirdClass, FrontArmStand }
        };
    }

    private void InitializePositionSprites()
    {
        PositionSprites = new Dictionary<DiveType, Dictionary<DivePosition, Sprite>>
        {
            { DiveType.FirstClass, new Dictionary<DivePosition, Sprite>
                {
                    { DivePosition.Tuck, TuckFront },
                    { DivePosition.Pike, PikeFront },
                    { DivePosition.Entrance, EntranceFront }
                }
            },
            { DiveType.SecondClass, new Dictionary<DivePosition, Sprite>
                {
                    { DivePosition.Tuck, TuckBack },
                    { DivePosition.Pike, PikeBack },
                    { DivePosition.Entrance, EntranceBack }
                }
            },
            { DiveType.ThirdClass, new Dictionary<DivePosition, Sprite>
                {
                    { DivePosition.Tuck, TuckFront },
                    { DivePosition.Pike, PikeFront },
                    { DivePosition.Entrance, EntranceFront }
                }
            },
            { DiveType.FourthClass, new Dictionary<DivePosition, Sprite>
                {
                    { DivePosition.Tuck, TuckBack },
                    { DivePosition.Pike, PikeBack },
                    { DivePosition.Entrance, EntranceBack }
                }
            },
            { DiveType.SixthFrontClass, new Dictionary<DivePosition, Sprite>
                {
                    { DivePosition.Tuck, TuckFront },
                    { DivePosition.Pike, PikeFront },
                    { DivePosition.Entrance, EntranceFront }
                }
            },
            { DiveType.SixthBackClass, new Dictionary<DivePosition, Sprite>
                {
                    { DivePosition.Tuck, TuckBack },
                    { DivePosition.Pike, PikeBack },
                    { DivePosition.Entrance, EntranceBack }
                }
            },
            { DiveType.SixthThirdClass, new Dictionary<DivePosition, Sprite>
                {
                    { DivePosition.Tuck, TuckFront },
                    { DivePosition.Pike, PikeFront },
                    { DivePosition.Entrance, EntranceFront }
                }
            }
        };
    }

    public Sprite UnfoldSprite(Sprite sprite)
    {
        var texture = sprite.texture;
        var unfoldedTexture = new Texture2D(texture.width, texture.height);

        for (var y = 0; y < texture.height; y++)
        {
            for (var x = 0; x < texture.width; x++)
                unfoldedTexture.SetPixel(texture.width - 1 - x, y, texture.GetPixel(x, y));
        }
        
        unfoldedTexture.Apply();
        
        var rect = new Rect(0, 0, unfoldedTexture.width, unfoldedTexture.height);
        var spriteCenter = new Vector2(0.5f, 0.5f);
        var unfoldedSprite = Sprite.Create(unfoldedTexture, rect, spriteCenter);

        return unfoldedSprite;
    }
}