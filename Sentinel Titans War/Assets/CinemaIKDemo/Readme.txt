CinemaIK gives you the opportunity to handle a character's IK pass to make animations more alive. For example, the main hero standing in front of the camera, let us imagine that all you have is the standard idle animation, its not bad in terms of gameplay but not good enough for the cutscene. So what are you going to do? You can duplicate that animation clip and make new specific idle animation for that cutscene, but it's not efficient. What if you have 10, 20, or maybe 100 cutscenes? And all you need is slightly change animations like turn head, blink, or hand/leg movements. That when CinemaIK comes in handy. You don't need to create new animation clips for your characters and on top of that making, new animations with CinemaIK is extremely easy. But it has limitations. You can't make an animation from scratch with it, you need a base, like idle animation. Be aware this tool is more targeted to use in pair with the Timeline. You can try to use it in another way, but 100% working it's not guaranteed.

To understand how its working please watch/read tutorials and open demo scene.

Components:
CinemaIK - main component. Place on separate object from you character so you can animate it independently.
CinemaIKAnchor - animator component anchor. Since we use separate animator on CinemaIK we cant directly pass IK data on character. So we just pass IK data to anchor which is pass it further to the character's animator.

Other scripts:
IKData - class for storing IK data, like poses and weights.
CinemaIKEditor - custom inspector for CinemaIK.

Tutorial video - https://youtu.be/P7cNpkWVpFI

Thanks to the following studios for the free assets used in the demo:
- Character model "Man in a Suit" by "Studio New Punch" - https://assetstore.unity.com/packages/3d/characters/humanoids/man-in-a-suit-51662
- Laptop model "Free Laptop" by "Vertex Studio" - https://assetstore.unity.com/packages/3d/props/electronics/free-laptop-90315
- Chair and table model "Folding Table and Chair PBR" by "devotid" - https://assetstore.unity.com/packages/3d/props/furniture/folding-table-and-chair-pbr-111726
- Mug model "YGS Mugs" by "YGS Assets" - https://assetstore.unity.com/packages/3d/props/interior/ygs-mugs-96665