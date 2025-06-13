using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

namespace KF
{
    public class WorldSaveGameManager : MonoBehaviour
    {
        public static WorldSaveGameManager instance;

        public PlayerManager player;

        [Header("SAVE/LOAD")]
        [SerializeField] bool saveGame;
        [SerializeField] bool loadGame;

        [Header("World Scene Index")]
        [SerializeField] int worldSceneIndex = 1;

        [Header("Save Data Writer")]
        private SaveFileDataWriter saveFileDataWriter;

        [Header("Current Character Data")]
        public CharacterSlot currentCharacterSlotBeingUsed;
        public CharacterSaveData currentCharacterData;
        private string saveFileName;

        [Header("Character Slots")]
        public CharacterSaveData characterSlot01;
        public CharacterSaveData characterSlot02;
        public CharacterSaveData characterSlot03;
        public CharacterSaveData characterSlot04;
        public CharacterSaveData characterSlot05;

        private void Awake()
        {
            //There can only be one instance of this script at one time. If there's another, destroy it.
            if (instance == null)
            {
                instance = this;
            } else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            LoadAllCharacterSlots();
        }

        private void Update()
        {
            if (saveGame)
            {
                saveGame = false;
                SaveGame();
            }

            if (loadGame)
            {
                loadGame = false;
                LoadGame();
            }
        }

        public string DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot characterSlot)
        {
            int index = (int)characterSlot;
            return $"characterSlot_{index:D2}";
        }

        public void AttemptToCreateNewGame()
        {
            saveFileDataWriter = new SaveFileDataWriter();
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;

            for (int i = 1; i <= 10; i++)
            {
                CharacterSlot slot = (CharacterSlot)i;
                string fileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(slot);

                saveFileDataWriter.saveFileName = fileName;

                if (!saveFileDataWriter.CheckToSeeIfFileExists())
                {
                    currentCharacterSlotBeingUsed = slot;
                    currentCharacterData = new CharacterSaveData();
                    StartCoroutine(LoadWorldScene());
                    return;
                }
            }

            TitleScreenManager.instance.DisplayNoFreeCharacterSlotsNoti();
        }
        public void LoadGame ()
        {
            // LOADING A FILE WITH A FILE NAME DEPENDING ON WHICH SLOT YOU'RE USING
            saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(currentCharacterSlotBeingUsed);

            saveFileDataWriter = new SaveFileDataWriter();
            // GENERALLY WORKS ON MULTIPLE MACHINE (Application.persistentDataPath)
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
            saveFileDataWriter.saveFileName = saveFileName;
            currentCharacterData = saveFileDataWriter.LoadSaveFile();

            StartCoroutine(LoadWorldScene());
        }

        public void SaveGame()
        {
            // SAVE THE CURRENT FILE UNDER A FILE NAME DEPENDING ON WHICH SLOT WE ARE USING
            saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(currentCharacterSlotBeingUsed);

            saveFileDataWriter = new SaveFileDataWriter();
            // GENERALLY WORKS ON MULTIPLE MACHINE (Application.persistentDataPath)
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
            saveFileDataWriter.saveFileName = saveFileName;

            // PASS THE PLAYERS INFO, FROM GAME, TO THEIR SAVE FILE
            player.SaveGameDataToCurrentCharacterData(ref currentCharacterData);

            // WRITE THAT INFO ONTO A JSON FILE, SAVED TO THIS MACHINE
            saveFileDataWriter.CreateNewCharacterSaveFile(currentCharacterData);
        }

        public void DeleteGame(CharacterSlot characterSlot) {
            // CHOOSE A FILE TO DELETE BASED ON THE NAME
            saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            saveFileDataWriter = new SaveFileDataWriter();
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
            saveFileDataWriter.saveFileName = saveFileName;

            saveFileDataWriter.DeleteSaveFile();
        }

        // LOAD ALL CHARACTER PROFILES ON DEVICE WHEN STARTING GAME
        private void LoadAllCharacterSlots()
        {
            saveFileDataWriter = new SaveFileDataWriter();
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;

            for (int i = 1; i <= 5; i++)
            {
                CharacterSlot slot = (CharacterSlot)i;
                string fileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(slot);
                saveFileDataWriter.saveFileName = fileName;

                CharacterSaveData data = saveFileDataWriter.LoadSaveFile();

                switch (slot)
                {
                    case CharacterSlot.CharacterSlot_01: characterSlot01 = data; break;
                    case CharacterSlot.CharacterSlot_02: characterSlot02 = data; break;
                    case CharacterSlot.CharacterSlot_03: characterSlot03 = data; break;
                    case CharacterSlot.CharacterSlot_04: characterSlot04 = data; break;
                    case CharacterSlot.CharacterSlot_05: characterSlot05 = data; break;
                }
            }
        }

        public IEnumerator LoadWorldScene()
        {
            //AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);
            
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(currentCharacterData.sceneIndex);
            
            player.LoadGameDataFromCurrentCharacterData(ref currentCharacterData);
            yield return null;
        }
        
        public int GetWorldSceneIndex()
        {
            return worldSceneIndex;
        }
    }
}