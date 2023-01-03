// SPDX-License-Identifier: MIT

pragma solidity 0.8.10;

library EIP2 {

    function Sort(uint[] memory arr) external pure returns (uint[] memory) {
        uint[] memory sorted = arr;
        for (uint i = 0; i < sorted.length - 1; i++) {
            for (uint j = i + 1; j < sorted.length; j++) {
                if (sorted[i] > sorted[j]) {
                    uint temp = sorted[i];
                    sorted[i] = sorted[j];
                    sorted[j] = temp;
                }
            }
        }
        return sorted;
    }
}
