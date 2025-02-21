﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BE2_DragBlock : MonoBehaviour, I_BE2_Drag
{
    RectTransform _rectTransform;
    BE2_DragDropManager _dragDropManager;
    Transform _transform;
    public Transform Transform => _transform ? _transform : transform;
    public Vector2 RayPoint => _rectTransform.position;
    public I_BE2_Block Block { get; set; }
    public List<I_BE2_Block> ChildBlocks { get; set; }

    void Awake()
    {
        _transform = transform;
        _rectTransform = GetComponent<RectTransform>();
        Block = GetComponent<I_BE2_Block>();
    }

    void Start()
    {
        _dragDropManager = BE2_DragDropManager.instance;
    }

    //void Update()
    //{
    //    
    //}

    public void OnPointerDown()
    {
        _dragDropManager = BE2_DragDropManager.instance;

        ChildBlocks = new List<I_BE2_Block>();
        ChildBlocks.AddRange(GetComponentsInChildren<I_BE2_Block>());
    }

    public void OnRightPointerDownOrHold()
    {
        BE2_UI_ContextMenuManager.instance.OpenContextMenu(0, Block);
    }

    public void OnDrag()
    {
        if (!_isDetectingSpot)
            StartCoroutine(DetectSpotOnEndOfFrame());
    }

    bool _isDetectingSpot = false;
    // v2.1 - detection of spot on drag of Block moved to coroutine and performed on end of frame to avoid glith on detecting new spot
    IEnumerator DetectSpotOnEndOfFrame()
    {
        _isDetectingSpot = true;
        yield return new WaitForEndOfFrame();

        if (Transform.parent != _dragDropManager.DraggedObjectsTransform)
            Transform.SetParent(_dragDropManager.DraggedObjectsTransform, true);

        I_BE2_Spot spot = _dragDropManager.Raycaster.FindClosestSpotForBlock(this, _dragDropManager.detectionDistance);

        Transform ghostBlockTransform = _dragDropManager.GhostBlockTransform;
        if (spot is BE2_SpotBlockBody && spot.Block != Block && !spot.Block.ToString().Contains("HorizontalBlock Ins Function"))
        {
            ghostBlockTransform.SetParent(spot.Transform);
            ghostBlockTransform.localScale = Vector3.one;
            ghostBlockTransform.gameObject.SetActive(true);
            ghostBlockTransform.SetSiblingIndex(0);

            _dragDropManager.CurrentSpot = spot;
        }
        else if (spot is BE2_SpotOuterArea)
        {
            ghostBlockTransform.SetParent(spot.Block.Transform.parent);
            ghostBlockTransform.localScale = Vector3.one;
            ghostBlockTransform.gameObject.SetActive(true);
            ghostBlockTransform.SetSiblingIndex(spot.Block.Transform.GetSiblingIndex() + 1);

            spot.Block.ParentSection.UpdateLayout();
            _dragDropManager.CurrentSpot = spot;
        }
        else
        {
            ghostBlockTransform.gameObject.SetActive(false);
            ghostBlockTransform.SetParent(_dragDropManager.ProgrammingEnv.Transform);
            _dragDropManager.CurrentSpot = null;
        }
        _isDetectingSpot = false;
    }

    public void OnPointerUp()
    {
        if (_dragDropManager.CurrentSpot != null)
        {
            if (_dragDropManager.CurrentSpot is BE2_SpotBlockBody)
            {
                Transform.SetParent(_dragDropManager.CurrentSpot.Transform);
                Transform.SetSiblingIndex(0);

                _dragDropManager.CurrentSpot = null;
            }
            else
            {
                Transform.SetParent(_dragDropManager.CurrentSpot.Block.Transform.parent);
                Transform.SetSiblingIndex(_dragDropManager.CurrentSpot.Block.Transform.GetSiblingIndex() + 1);

                _dragDropManager.CurrentSpot = null;
            }
        }
        else
        {
            I_BE2_Spot spot = _dragDropManager.Raycaster.GetSpotAtPosition(RayPoint);
            if (spot != null)
            {
                I_BE2_ProgrammingEnv programmingEnv = spot.Transform.GetComponentInParent<I_BE2_ProgrammingEnv>();
                if (programmingEnv == null && spot.Transform.GetChild(0) != null)
                    programmingEnv = spot.Transform.GetChild(0).GetComponentInParent<I_BE2_ProgrammingEnv>();

                if (programmingEnv != null)
                    Transform.SetParent(programmingEnv.Transform);
                else
                    Destroy(Transform.gameObject);
            }
            else
            {
                Destroy(Transform.gameObject);
            }
        }
    }
}
