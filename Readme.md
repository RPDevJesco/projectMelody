# Project Melody

## Overview

Project Melody is a comprehensive MIDI file generation tool designed for musicians, composers, and hobbyists. It leverages the MIDI protocol to create versatile musical pieces, offering functionality to generate chords, arpeggios, and melodies. The project is built using C# and utilizes the NAudio library for MIDI file creation and manipulation.

## Features

### MIDI File Generation
- **Chord Generation:** Produces MIDI files containing chord progressions. Users can specify the root note, scale type, and number of chords. The chords are randomly selected from a range of chord types and are transposed to fit within the frequency range of a selected instrument.
- **Arpeggio Generation:** Similar to chord generation, but instead of playing chords, the notes of each chord are played sequentially to form arpeggios. This function allows for the specification of the number of notes per arpeggio.
- **Melody Generation:** Generates melodic sequences based on a given scale and root note. The melody is constrained within the range of the selected instrument, ensuring playability.

### Instrument Frequency Ranges
- The project includes a comprehensive list of musical instruments and their corresponding frequency ranges. This information is utilized to ensure that the generated MIDI notes are within the playable range of the chosen instrument.

### Rhythm Patterns
- Includes predefined rhythm patterns for melody generation, providing variety and musicality to the generated sequences.

### Scale and Chord Theory Integration
- The project incorporates music theory concepts, including various scale types (Major, Minor, Blues, Pentatonic, etc.) and an extensive list of chord types (Triads, Seventh, Ninth, etc.), offering a wide range of musical possibilities.

### Extensibility
- Designed to be easily extendable for additional scales, chord types, rhythm patterns, and instrument ranges.

## Usage

To generate a MIDI file, users can specify parameters such as the root note, scale type, instrument, tempo, and the type of sequence (chord, arpeggio, or melody). The program then processes these inputs to create a MIDI file that can be used for various musical applications.

## Example

```csharp
MidiGenerator.GenerateMidiFile(
    "TestMidi.mid", 
    BasicNote.C, 
    ScaleType.Major, 
    InstrumentFactory.GetInstrument("Violin"), 
    120, 
    "chord", 
    8
);
```

This example generates a MIDI file with a C Major chord progression played on a Violin at 120 BPM, containing 8 chords.
Project Melody opens up new avenues for digital music composition, offering a unique blend of programmable music theory and MIDI generation. 
Whether for educational purposes, composition, or experimentation, this tool provides a versatile platform for exploring the world of music.


## MIDI Note Frequencies

The following are MIDI note numbers for various musical notes:

### Every A Note
- A0: 21
- A1: 33
- A2: 45
- A3: 57
- A4: 69 (Standard Tuning for Guitar)
- A5: 81
- A6: 93
- A7: 105

### Every A#/Bb Note
- A#/Bb0: 22
- A#/Bb1: 34
- A#/Bb2: 46
- A#/Bb3: 58
- A#/Bb4: 70
- A#/Bb5: 82
- A#/Bb6: 94
- A#/Bb7: 106

### Every B Note
- B0: 23
- B1: 35
- B2: 47
- B3: 59
- B4: 71
- B5: 83
- B6: 95
- B7: 107

### Every C Note
- C1: 24
- C2: 36
- C3: 48
- C4: 60 (Middle C)
- C5: 72
- C6: 84
- C7: 96
- C8: 108

### Every C#/Db Note
- C#/Db1: 25
- C#/Db2: 37
- C#/Db3: 49
- C#/Db4: 61
- C#/Db5: 73
- C#/Db6: 85
- C#/Db7: 97
- C#/Db8: 109

### Every D Note
- D1: 26
- D2: 38
- D3: 50
- D4: 62
- D5: 74
- D6: 86
- D7: 98
- D8: 110

### Every D#/Eb Note
- D#/Eb1: 27
- D#/Eb2: 39
- D#/Eb3: 51
- D#/Eb4: 63
- D#/Eb5: 75
- D#/Eb6: 87
- D#/Eb7: 99
- D#/Eb8: 111

### Every E Note
- E1: 28
- E2: 40
- E3: 52
- E4: 64
- E5: 76
- E6: 88
- E7: 100
- E8: 112

### Every F Note
- F1: 29
- F2: 41
- F3: 53
- F4: 65
- F5: 77
- F6: 89
- F7: 101
- F8: 113

### Every F#/Gb Note
- F#/Gb1: 30
- F#/Gb2: 42
- F#/Gb3: 54
- F#/Gb4: 66
- F#/Gb5: 78
- F#/Gb6: 90
- F#/Gb7: 102
- F#/Gb8: 114

### Every G Note
- G1: 31
- G2: 43
- G3: 55
- G4: 67
- G5: 79
- G6: 91
- G7: 103
- G8: 115

### Every G#/Ab Note
- G#/Ab1: 32
- G#/Ab2: 44
- G#/Ab3: 56
- G#/Ab4: 68
- G#/Ab5: 80
- G#/Ab6: 92
- G#/Ab7: 104
- G#/Ab8: 116

## Instrument Frequency Ranges

- **Guitar (Standard Tuning 6 string):** E2 (40) to E4 (64)
- **Guitar (Standard Tuning 7 string):** B1 (35) to E4 (64)
- **Bass Guitar (Standard Tuning 4 string):** E1 (28) to G2 (43)
- **Bass Guitar (Standard Tuning 5 string):** B0 (23) to G2 (43)
- **Violin:** G3 (55) to E7 (100)
- **Viola:** C3 (48) to C6 (84)
- **Cello:** C2 (36) to E5 (76)
- **Double Bass:** E1 (28) to B3 (59)
- **Flute:** C4 (60) to C7 (96)
- **Oboe:** Bb3 (58) to F6 (89)
- **English Horn:** Eb3 (51) to Bb5 (82)
- **Clarinet (Bb):** D3 (50) to Bb6 (94)
- **Bass Clarinet (Bb):** D2 (38) to F5 (77)
- **Bassoon:** Bb1 (34) to Bb5 (82)
- **Contrabassoon:** Bb0 (22) to Eb3 (51)
- **Horn (double, F & Bb):** B1 (35) to F5 (77)
- **Trumpet (Bb):** E3 (52) to Bb5 (82)
- **Trombone (tenor):** E2 (40) to Bb4 (70)
- **Trombone (bass):** B1 (35) to Bb4 (70)
- **Timpani:** F2 (41) to F4 (65)
- **Harp:** B0 (23) to G#7 (104)

This document provides reference information for MIDI note frequencies and the frequency range of various musical instruments.
