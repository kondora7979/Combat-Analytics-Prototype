# TACTICAL DRONE V4

Unity와 C#을 활용하여 제작한 **실시간 전투 데이터 분석 프로토타입**입니다.

MMORPG 보스 전투에서 발생하는 전투 데이터를 수집하고,  
극딜 성능, 스킬 사용, 생존 지표 등을 실시간으로 분석하는 시스템을 구현했습니다.

---

## 프로젝트 개요

- 개발 환경: Unity 6
- 사용 언어: C#
- 개발 형태: 개인 프로젝트
- 주요 목적:
  - 전투 이벤트 기반 데이터 수집
  - 극딜 성능 분석
  - 생존 데이터 분석
  - 전투 분석 프로그램의 모듈화

---

## 주요 기능

### 1. Burst Analyzer

특정 스킬 사용을 기준으로 극딜 측정을 시작합니다.

측정 시간 동안 발생한 피해량을 기록하고 다음 데이터를 계산합니다.

- 총 피해량
- DPS
- 스킬별 피해량
- 극딜 회차 기록
- 이전 극딜과 현재 극딜 비교

관련 코드:

`BurstAnalyzer.cs`  
`BurstComparisonHUD.cs`

---

### 2. Skill Monitor

전투 중 발생하는 스킬 사용 이벤트를 감지하고 기록합니다.

각 시스템이 직접 플레이어를 검사하지 않고  
`CombatEventHub`를 통해 전달되는 이벤트를 활용하도록 구성했습니다.

관련 코드:

`SkillMonitor.cs`

---

### 3. Survival Monitor

플레이어가 받은 피해를 기록하여 생존 관련 데이터를 분석합니다.

- 피격 횟수
- 누적 피해량
- 평균 피해량
- 최대 피해량

관련 코드:

`SurvivalMonitor.cs`  
`PlayerHealth.cs`

---

### 4. Combat Event System

전투 시스템 간의 직접적인 의존성을 줄이기 위해  
`CombatEventHub`를 중심으로 이벤트를 전달하도록 구현했습니다.

예시 구조:

```text
Player / Boss
     │
     │ Combat Event
     ▼
CombatEventHub
     │
     ├── BurstAnalyzer
     ├── SkillMonitor
     └── SurvivalMonitor
```

각 분석 시스템은 자신에게 필요한 전투 이벤트를 구독하여 처리합니다.

관련 코드:

`CombatEventHub.cs`

---

### 5. Drone Program System

전투 분석 기능을 각각의 Drone Program으로 관리하도록 구성했습니다.

Drone Hardware 등급에 따라 사용할 수 있는 최대 Program Cost가 변경되며,  
`DroneProgramManager`가 프로그램의 사용 여부와 Cost를 관리합니다.

관련 코드:

`DroneHardware.cs`  
`DroneProgramBase.cs`  
`DroneProgramManager.cs`

---

## 코드 구조

```text
Scripts/
├── BossAttackTest.cs
├── BossHealth.cs
├── BossHealthBar.cs
├── BurstAnalyzer.cs
├── BurstComparisonHUD.cs
├── CombatEventHub.cs
├── DroneHardware.cs
├── DroneHUD.cs
├── DroneProgramBase.cs
├── DroneProgramHUD.cs
├── DroneProgramManager.cs
├── PlayerCombatTest.cs
├── PlayerHealth.cs
├── SkillMonitor.cs
└── SurvivalMonitor.cs
```

---

## 구현 내용

프로젝트를 구현하면서 다음 C# 및 Unity 기능을 활용했습니다.

- C# Event
- Event Subscription
- Class
- Property
- List
- Dictionary
- Inheritance
- Unity MonoBehaviour
- UI와 데이터 처리 로직 분리

---

## 현재 구현 상태

- ✅ 전투 이벤트 전달
- ✅ 극딜 측정
- ✅ DPS 계산
- ✅ 스킬별 피해 기록
- ✅ 극딜 회차 기록 및 비교
- ✅ 생존 데이터 분석
- ✅ Drone Program 관리
- ✅ Drone Hardware / Program Cost 연동
- ✅ 전투 분석 HUD
