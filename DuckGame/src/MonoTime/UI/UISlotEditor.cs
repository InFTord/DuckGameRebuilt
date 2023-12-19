﻿using System;

namespace DuckGame
{
    public class UISlotEditor : UIMenu
    {
        private UIMenu _closeMenu;
        //private Rectangle _captureRectangle;
        //private BitmapFont _littleFont;
        //private BitmapFont _littleFont2;
        public static int _slot = 0;
        public static int _indexX;
        public static int _indexY;
        //private Vec2 _rectPosition;
        public static bool editingSlots = false;
        public bool finished;
        private bool _selectionChanged = true;
        private bool _selectionChangedDueToSpectatorSwap = false;
        private bool _justOpened;
        private Profile _profileThatJustStartedBeingSpectatorSwapped = null;
        //private bool _showedWarning;
        public int[,] kIndexMap;
        public int maxside;

        public static int hoveringSlot = -1;
        //= new int[3, 3]
        //{
        //  {
        //    0,
        //    1,
        //    4
        //  },
        //  {
        //    2,
        //    3,
        //    5
        //  },
        //  {
        //    6,
        //    -1,
        //    7
        //  }
        //}
        public UISlotEditor(UIMenu closeMenu, float xpos, float ypos, float wide = -1f, float high = -1f)
          : base("", xpos, ypos, wide, high)
        {
            // float num = 38f;
            //this._captureRectangle = new Rectangle((float)(int)(Layer.HUD.camera.width / 2.0 - num / 2.0), (float)(int)(Layer.HUD.camera.height / 2.0 - num / 2.0), (float)(int)num, (float)(int)num);
            _closeMenu = closeMenu;
            //this._littleFont = new BitmapFont("smallBiosFontUI", 7, 5);
            //this._littleFont2 = new BitmapFont("smallBiosFont", 7, 6);
            int total = DG.MaxPlayers;
            maxside = (int)Math.Ceiling(Math.Sqrt(total + 1));
            Vec2 Position = new Vec2(0f, 0f);
            kIndexMap = new int[maxside, maxside];
            for (int i = 0; i < maxside; i++)
            {
                for (int j = 0; j < maxside; j++)
                {
                    kIndexMap[i, j] = -1;
                }
            }
            int slotindex = 0;
            for (int i = 0; i < total + 1; i++)
            {
                if (i != 7)
                {
                    try
                    {
                        kIndexMap[(int)Position.y, (int)Position.x] = slotindex;
                    }
                    catch
                    {
                        DevConsole.Log("uhh");
                    }
                    slotindex += 1;
                }
                if (Position.x == Position.y + 1) //reset one lower and all the way to to the left
                {
                    Position.x = 0;
                    Position.y += 1;
                }
                else if (Position.x > Position.y)//go down
                {
                    Position.y += 1;
                }
                else
                {
                    if (Position.x == Position.y)//reset to top row
                    {
                        Position.y = 0;
                    }
                    Position.x += 1; //go right
                }
            }
        }

        public override void Open()
        {
            HUD.CloseAllCorners();
            editingSlots = true;
            _justOpened = true;
            HUD.AddCornerControl(HUDCorner.TopRight, "@CANCEL@DONE");
            MonoMain.doPauseFade = false;
            base.Open();
        }

        public override void Close()
        {
            HUD.CloseAllCorners();
            editingSlots = false;
            hoveringSlot = -1;
            MonoMain.doPauseFade = true;
            base.Close();
        }

        public override void Update()
        {
            if (open)
            {
                int slot = _slot;
                if (Input.Pressed(Triggers.MenuLeft))
                {
                    if (_indexX == 2 && _indexY == 2)
                    {
                        _indexX = 0;
                    }
                    else
                    {
                        --_indexX;
                        if (_indexX < 0)
                            _indexX = 0;
                        if (kIndexMap[_indexY, _indexX] == -1)
                            ++_indexX;
                    }
                }
                else if (Input.Pressed(Triggers.MenuRight))
                {
                    if (_indexX == 0 && _indexY == 2)
                    {
                        _indexX = 2;
                    }
                    else
                    {
                        ++_indexX;
                        if (_indexX > maxside - 1)
                            _indexX = maxside - 1;
                        if (kIndexMap[_indexY, _indexX] == -1)
                            --_indexX;
                    }
                }
                else if(Input.Pressed(Triggers.MenuUp))
                {
                    if (_indexX == 1 && _indexY == 3)
                    {
                        _indexY = 1;
                    }
                    else
                    {
                        --_indexY;
                        if (_indexY < 0)
                            _indexY = 0;
                        if (kIndexMap[_indexY, _indexX] == -1)
                            ++_indexY;
                    }
                }
                else if(Input.Pressed(Triggers.MenuDown))
                {
                    if (_indexX == 1 && _indexY == 1)
                    {
                        _indexY = 1;
                        if (maxside > 3 && kIndexMap[3, _indexX] != -1)
                        {
                            _indexY = 3;
                        }
                    }
                    else
                    {
                        ++_indexY;
                        if (_indexY > maxside - 1)
                            _indexY = maxside - 1;
                        if (kIndexMap[_indexY, _indexX] == -1)
                            --_indexY;
                    }
                }
                _slot = kIndexMap[_indexY, _indexX];
                hoveringSlot = _slot;
                if (DuckNetwork.GetSlotGenre(_slot) != DuckNetwork.GetSlotGenre(slot))
                {
                    _selectionChanged = true;
                }

                if (_slot >= 0)
                {
                    if (_selectionChanged || _justOpened || (DuckNetwork.SpectatorSwapFinished(_profileThatJustStartedBeingSpectatorSwapped) && _selectionChangedDueToSpectatorSwap))
                    {
                        if (DuckNetwork.IsEmptySlot(_slot))
                        {
                            HUD.AddCornerControl(HUDCorner.BottomLeft, "@GRAB@APPLY TO ALL");
                            HUD.AddCornerControl(HUDCorner.BottomRight, "@SELECT@TOGGLE");
                        }
                        else
                        {
                            if ((DuckNetwork.IsEmptySlot(slot) && Network.canSetObservers) || _justOpened)
                                HUD.AddCornerControl(HUDCorner.BottomRight, "@MENU1@MAKE SPECTATOR");
                            if (DuckNetwork.IsHostSlot(_slot))
                                HUD.CloseCorner(HUDCorner.BottomLeft);
                            else
                                if (DuckNetwork.IsLocalSlot(_slot))
                                    HUD.AddCornerControl(HUDCorner.BottomLeft, "@MENU2@KICK");
                                else
                                    HUD.AddCornerControl(HUDCorner.BottomLeft, "@MENU2@KICK @RAGDOLL@BAN");
                        }
                        _selectionChanged = false;
                        _justOpened = false;
                        _selectionChangedDueToSpectatorSwap = false;
                    }

                    if (DuckNetwork.profiles[_slot].readyForSpectatorChange && Network.canSetObservers && Input.Pressed(Triggers.Menu1) && DuckNetwork.profiles[_slot].connection != null)
                    {
                        _selectionChangedDueToSpectatorSwap = true;
                        _profileThatJustStartedBeingSpectatorSwapped = DuckNetwork.profiles[_slot];
                        DuckNetwork.MakeSpectator(_profileThatJustStartedBeingSpectatorSwapped);
                        SFX.Play("menuBlip01");
                    }
                    else if (DuckNetwork.profiles[_slot].connection == null)
                    {
                        if (Input.Pressed(Triggers.Select))
                        {
                            int num = (int)(DuckNetwork.profiles[_slot].slotType + 1);
                            if (DuckNetwork.profiles[_slot].reservedUser != null && num == 5)
                                ++num;
                            if (DuckNetwork.profiles[_slot].reservedUser == null && num >= 5 || DuckNetwork.profiles[_slot].reservedUser != null && num > 6)
                                num = 0;
                            DuckNetwork.profiles[_slot].slotType = (SlotType)num;
                            DuckNetwork.ChangeSlotSettings();
                            SFX.Play("menuBlip01");
                        }
                        else if (Input.Pressed(Triggers.Grab))
                        {
                            SlotType currentSlotType = DuckNetwork.profiles[_slot].slotType;
                            for (int x = 0; x < maxside; x++)
                            {
                                for (int y = 0; y < maxside; y++)
                                {
                                    int sl = kIndexMap[y, x];
                                    if (sl != -1 && sl != _slot && DuckNetwork.profiles[sl].connection == null)
                                        DuckNetwork.profiles[sl].slotType = currentSlotType;
                                }
                            }
                            SFX.Play("menuBlip01");
                        }
                        else if (DGRSettings.dubberspeed)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                if (Keyboard.Pressed(Keys.D1 + i) && DuckNetwork.profiles[_slot].slotType != (SlotType)i)
                                {
                                    DuckNetwork.profiles[_slot].slotType = (SlotType)i;
                                    DuckNetwork.ChangeSlotSettings();
                                    SFX.Play("menuBlip01");
                                    break;
                                }
                            }
                        }
                    }
                    else if (Input.Pressed(Triggers.Menu2))
                        DuckNetwork.Kick(DuckNetwork.profiles[_slot]);
                    else if (Input.Pressed(Triggers.Ragdoll) && DuckNetwork.profiles[_slot].connection != DuckNetwork.localConnection)
                        DuckNetwork.Ban(DuckNetwork.profiles[_slot]);
                }
                if (Input.Pressed(Triggers.Cancel))
                {
                    SFX.Play("consoleCancel");
                    new UIMenuActionOpenMenu(this, _closeMenu).Activate();
                }
            }
            base.Update();
        }

        public override void Draw()
        {
        }
    }
}
