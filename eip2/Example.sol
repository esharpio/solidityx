// SPDX-License-Identifier: MIT

pragma solidity 0.8.10;

import "./EIP2.sol";

contract Example {
    uint[] public arr = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
    uint[] public sorted;

    function Sort() external {
        sorted = EIP2.Sort(arr);
    }
}