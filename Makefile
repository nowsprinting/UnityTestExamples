# Copyright (c) 2021-2023 Koji Hasegawa.
# This software is released under the MIT License.

PROJECT_HOME?=$(shell dirname $(realpath $(lastword $(MAKEFILE_LIST))))
BUILD_DIR?=$(PROJECT_HOME)/Builds
LOG_DIR?=$(PROJECT_HOME)/Logs
UNITY_VERSION?=$(shell grep 'm_EditorVersion:' $(PROJECT_HOME)/ProjectSettings/ProjectVersion.txt | grep -o -E '\d{4}\.\d+\.\d+[abfp]\d+')

# Code Coverage report filter (comma separated)
# see: https://docs.unity3d.com/Packages/com.unity.testtools.codecoverage@1.2/manual/CoverageBatchmode.html
EMBEDDED_PACKAGE_ASSEMBLIES?=$(shell echo $(shell find ./Packages -name "*.asmdef" | sed -e s/.*\\//\+/ | sed -e s/\\.asmdef// | sed -e s/^.*\\.Tests//) | sed -e s/\ /,/g)
COVERAGE_ASSEMBLY_FILTERS?=+<assets>,$(EMBEDDED_PACKAGE_ASSEMBLIES),+LocalPackageSample*,-*.Tests

# -nographics` option
ifdef NOGRAPHICS
NOGRAPHICS=-nographics
endif

# -testCategory option. see https://docs.unity3d.com/Packages/com.unity.test-framework@1.3/manual/reference-command-line.html#testcategory
ifdef CATEGORY
TEST_CATEGORY=-testCategory "$(CATEGORY)"
else
TEST_CATEGORY=-testCategory "!IgnoreCI;!Integration"
endif

# -testFilter option. see https://docs.unity3d.com/Packages/com.unity.test-framework@1.3/manual/reference-command-line.html#testfilter
ifdef FILTER
TEST_FILTER=-testFilter "$(FILTER)"
endif

# -assemblyNames option. see https://docs.unity3d.com/Packages/com.unity.test-framework@1.3/manual/reference-command-line.html#assemblynames
ifdef ASSEMBLY
ASSEMBLY_NAMES=-assemblyNames "$(ASSEMBLY)"
endif

UNAME := $(shell uname)
ifeq ($(UNAME), Darwin)
UNITY_HOME=/Applications/Unity/HUB/Editor/$(UNITY_VERSION)/Unity.app/Contents
UNITY?=$(UNITY_HOME)/MacOS/Unity
UNITY_YAML_MERGE?=$(UNITY_HOME)/Tools/UnityYAMLMerge
STANDALONE_PLAYER=StandaloneOSX
endif
ifeq ($(UNAME), Linux)  # not test yet
UNITY_HOME=$HOME/Unity/Hub/Editor/<version>
UNITY?=$(UNITY_HOME)/Unity
UNITY_YAML_MERGE?=$(UNITY_HOME)/ # unknown
STANDALONE_PLAYER=StandaloneLinux64
endif

define base_arguments
-projectPath $(PROJECT_HOME) \
-logFile $(LOG_DIR)/$(TEST_PLATFORM).log
endef

define test_arguments
-batchmode \
$(NOGRAPHICS) \
-silent-crashes \
-stackTraceLogType Full \
-runTests \
$(TEST_CATEGORY) \
$(TEST_FILTER) \
$(ASSEMBLY_NAMES) \
-testPlatform $(TEST_PLATFORM) \
-testResults $(LOG_DIR)/$(TEST_PLATFORM)-results.xml \
-testHelperJUnitResults $(LOG_DIR)/$(TEST_PLATFORM)-junit-results.xml \
-testHelperScreenshotDirectory $(LOG_DIR)/Screenshots
endef

define test
  $(eval TEST_PLATFORM=$1)
  mkdir -p $(LOG_DIR)
  $(UNITY) \
    $(call base_arguments) \
    $(call test_arguments)
endef

define cover
  $(eval TEST_PLATFORM=$1)
  mkdir -p $(LOG_DIR)
  $(UNITY) \
    $(call base_arguments) \
    $(call test_arguments) \
    --burst-disable-compilation \
    -debugCodeOptimization \
    -enableCodeCoverage \
    -coverageResultsPath $(LOG_DIR) \
    -coverageOptions 'generateAdditionalMetrics;generateTestReferences;dontClear;assemblyFilters:$(COVERAGE_ASSEMBLY_FILTERS)'
endef

define cover_report
  mkdir -p $(LOG_DIR)
  $(UNITY) \
    $(call base_arguments) \
    -batchmode \
    -quit \
    -enableCodeCoverage \
    -coverageResultsPath $(LOG_DIR) \
    -coverageOptions 'generateHtmlReport;generateAdditionalMetrics;generateAdditionalReports;assemblyFilters:$(COVERAGE_ASSEMBLY_FILTERS)'
endef

.PHONY: usage
usage:
	@echo "Tasks:"
	@echo "  clean: Clean Build and Logs directories in created project."
	@echo "  setup_unityyamlmerge: Setup UnityYAMLMerge as mergetool in .git/config."
	@echo "  open: Open this project in Unity editor."
	@echo "  test_editmode: Run Edit Mode tests."
	@echo "  test_playmode: Run Play Mode tests."
	@echo "  cover_report: Create code coverage HTML report."
	@echo "  test: Run test_editmode, test_playmode, and cover_report. Recommended to use with '-k' option."
	@echo "  test_standalone_player: Run Play Mode tests on standalone player."
	@echo "  test_android: Run Play Mode tests on Android device."
	@echo "  test_ios: Run Play Mode tests on iOS device."

.PHONY: clean
clean:
	rm -rf $(BUILD_DIR)
	rm -rf $(LOG_DIR)

.PHONY: setup_unityyamlmerge
setup_unityyamlmerge:
	git config --local merge.tool "unityyamlmerge"
	git config --local mergetool.unityyamlmerge.trustExitCode false
	git config --local mergetool.unityyamlmerge.cmd '$(UNITY_YAML_MERGE) merge -p "$$BASE" "$$REMOTE" "$$LOCAL" "$$MERGED"'

.PHONY: open
open:
	mkdir -p $(LOG_DIR)
	$(UNITY) $(call base_arguments) &

.PHONY: test_editmode
test_editmode:
	$(call cover,editmode)

.PHONY: test_playmode
test_playmode:
	$(call cover,playmode)

.PHONY: cover_report
cover_report:
	$(call cover_report)

# Run Edit Mode and Play Mode tests with coverage and html-report by code coverage package.
# If you run this target with the `-k` option, if the Edit/Play Mode test fails,
# it will run through to Html report generation and return an exit code indicating an error.
.PHONY: test
test: test_editmode test_playmode cover_report

# Run Play Mode tests on standalone player
# Run test because code coverage package is not support run on standalone player.
.PHONY: test_standalone_player
test_standalone_player:
	$(call test,$(STANDALONE_PLAYER))

# Run Play Mode tests on Android device
# Run test because code coverage package is not support run on standalone player.
.PHONY: test_android
test_android:
	$(call test,Android)

# Run Play Mode tests on iOS device
# Run test because code coverage package is not support run on standalone player.
.PHONY: test_ios
test_ios:
	$(call test,iOS)
