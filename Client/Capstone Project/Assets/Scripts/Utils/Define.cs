﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum UIEvent
    {
        Click,
        Drag,

    }
    public enum MouseEvent
    {
        Press,
        Click,
    }
    public enum CameraMode
    {
        QuarterView,
        OverView,
        
    }

    public enum Map
    {
        MapWidth = 20,
    }

    public enum Direction
    {
        up = 0,
        right = 1,
        down = 2,
        left = 3,
    }


    public enum Setting // 맵 생성 위한 배경 타일과 블록 크기, 배경 타일에 대한 카메라 상대위치
    {
        BlockStartPosition = -75,
        BlockWidth = 4,
        CameraPositionY = 80,
    }

    public enum StageBlock // 스테이지 별 별 획득 위한 코드 블록 사용 개수 조건 설정
    {
        FirstStudyStage = 5,
        Challenge1 = 10,
        Challenge2 = 10,
        Challenge3 = 10,
        Challenge4 = 10,
        Challenge5 = 10,
        Challenge6 = 15,
        Challenge7 = 15,
        Challenge8 = 15,
        Challenge9 = 15,
        Challenge10 = 15,
    }

    public enum KnightTypeByStage
    {
        FirstStudyStage = 1,
        Challenge1 = 1,
        Challenge2 = 1,
        Challenge3 = 1,
        Challenge4 = 1,
        Challenge5 = 1,
        Challenge6 = 1,
        Challenge7 = 1,
        Challenge8 = 1,
        Challenge9 = 1,
        Challenge10 = 1,
    }

    public enum CharBtnState
    {
        Chooseable = 0,
        Chosen = 1,
        Locked = 2,
    }

    public enum MessageBoxState
    {
        Start = 0,
        End = 1,
    }
}
